using AR_IS.Models;
using AR_IS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "') ").ToList());
        }
        public ActionResult New(Customer Customer)
        {
            var viewModel = new CustomerVM
            {
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Customer = Customer
            };
            return View(viewModel);
        }
        public ActionResult Save(Customer Customer, ThirdLevel Thirdlevel, HttpPostedFileBase img)
        {
            string vardirection = "";
            string ImageName = "";
            string ImageName2 = "";
            string physicalpath;
            int account_no1;
            if (img != null)
            {
                ImageName = System.IO.Path.GetFileName(img.FileName);
                physicalpath = Server.MapPath("~/uploads/" + ImageName);
                img.SaveAs(physicalpath);
            }
            if (Customer.id == 0)
            {

                Thirdlevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM Thirdlevels where HeadId = 1 AND Comid='" + Session["Company"] + "'").FirstOrDefault();
                if (Thirdlevel.AccountNo == 0)
                {
                    account_no1 = Convert.ToInt32("1100001");
                }
                else
                {
                    account_no1 = Convert.ToInt32(Thirdlevel.AccountNo + 1);
                }
                Customer.AccountNo = account_no1;
                Customer.Image = ImageName;

                _context.tbl_Customer.Add(Customer);
                Customer.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("insert into Thirdlevels (AccountNo,FirstLevelId,HeadId,AccountTitle,AccountType,cr,dr,SecondLevelId,Comid) values(" + account_no1 + ",1001,1,N'" + Customer.Name + "','Customer',0,0,'1000002','" + Session["Company"] + "')");
                vardirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                var Customerdb = _context.tbl_Customer.Single(c => c.id == Customer.id);
                ImageName2 = _context.Database.SqlQuery<string>("SELECT     Image FROM   Customers  where AccountNo='" + Customer.AccountNo+ "'   AND Comid='" + Session["Company"] + "'").FirstOrDefault();
                if (ImageName != "")
                {
                    ImageName2 = ImageName;
                }
                Customerdb.Name = Customer.Name;
                Customerdb.Phone1 = Customer.Phone1;
                Customerdb.FatherName = Customer.FatherName;
                Customerdb.Phone2 = Customer.Phone2;
                Customerdb.Phone3 = Customer.Phone3;
                Customerdb.Email = Customer.Email;
                Customerdb.Image = Customer.Image;
                Customerdb.AccountNo = Customer.AccountNo;
                Customerdb.Address = Customer.Address;
                Customerdb.Town = Customer.Town;
                Customerdb.Province = Customer.Province;
                Customerdb.NTN = Customer.NTN;
                Customerdb.GST = Customer.GST;
                Customerdb.CNIC = Customer.CNIC;
                _context.Database.ExecuteSqlCommand("UPDATE Thirdlevels set AccountTitle = '" + Customer.Name + "' where AccountNo = " + Thirdlevel.AccountNo + "");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("update Customers set Image='" + ImageName2 + "' where AccountNo='" + Customer.AccountNo + "'  AND Comid='" + Session["Company"]+"'");
                vardirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            return RedirectToAction(vardirection, "Customer");
        }
        public ActionResult Edit(int? id)
        {
            var Customer = _context.Database.SqlQuery<Customer>("SELECT     *   FROM   Customers  where AccountNo='" + id + "' AND Comid='" + Session["Company"] +"' ").FirstOrDefault();
            if (Customer == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var viewModel = new CustomerVM
                {
                    Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                    Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                    Customer = Customer
                };
                return View("New", viewModel);
        }
        
    }
}