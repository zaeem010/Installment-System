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
    public class SupplierController : Controller
    {
        private ApplicationDbContext _context;

        public SupplierController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Supplier
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')  ").ToList());
        }
        public ActionResult New(Supplier Supplier)
        {
            var viewModel = new SupplierVM
            {
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces  ").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns").ToList(),
                Supplier = Supplier
            };
            return View(viewModel);
        }
        public ActionResult Save(Supplier Supplier, ThirdLevel Thirdlevel, HttpPostedFileBase img)
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
            if (Supplier.Id == 0)
            {
                Thirdlevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM Thirdlevels where HeadId = 2 AND Comid='" + Session["Company"] + "'").FirstOrDefault();
                if (Thirdlevel.AccountNo == 0)
                {
                    account_no1 = Convert.ToInt32("2100001");
                }
                else
                {
                    account_no1 = Convert.ToInt32(Thirdlevel.AccountNo + 1);
                }
                Supplier.AccountNo = account_no1;
                Supplier.Image = ImageName;
                _context.tbl_Supplier.Add(Supplier);
                Supplier.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("insert into Thirdlevels (AccountNo,FirstLevelId,HeadId,AccountTitle,AccountType,Cr,Dr,SecondLevelId,Comid) values(" + account_no1 + ",2001,2,N'" + Supplier.Name + "','Supplier',0,0,'2000001','" + Session["Company"] + "')");
                vardirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                var Supplierdb = _context.tbl_Supplier.Single(c => c.Id == Supplier.Id);
                ImageName2 = _context.Database.SqlQuery<string>("SELECT     Image FROM   Suppliers  where AccountNo=" + Supplier.AccountNo).FirstOrDefault();
                if (ImageName != "")
                {
                    ImageName2 = ImageName;
                }
                Supplierdb.Name = Supplier.Name;
                Supplierdb.Phone = Supplier.Phone;
                Supplierdb.Comname = Supplier.Comname;
                Supplierdb.Image = Supplier.Image;
                Supplierdb.AccountNo = Supplier.AccountNo;
                Supplierdb.Telephone = Supplier.Telephone;
                Supplierdb.Email = Supplier.Email;
                Supplierdb.Town = Supplier.Town;
                Supplierdb.Province = Supplier.Province;
                Supplierdb.Address = Supplier.Address;
                Supplierdb.CNIC = Supplier.CNIC;
                _context.Database.ExecuteSqlCommand("UPDATE Thirdlevels set AccountTitle = '" + Supplier.Name + "' where AccountNo = " + Thirdlevel.AccountNo + "");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("update Suppliers set Image='" + ImageName2 + "' where AccountNo=" + Supplier.AccountNo + ";");
                vardirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            return RedirectToAction(vardirection, "Supplier");
        }
        public ActionResult Edit(int? id)
        {
            var Supplier = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (AccountNo='"+id+ "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault();
            if (Supplier == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var viewModel = new SupplierVM
                {
                    Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces  ").ToList(),
                    Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns").ToList(),
                    Supplier = Supplier
                };
                return View("New", viewModel);
            }
        }
}
