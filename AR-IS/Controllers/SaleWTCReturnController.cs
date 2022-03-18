using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class SaleWTCReturnController : Controller
    {
        private ApplicationDbContext _context;

        public SaleWTCReturnController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: SaleWTCReturn
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<SaleWTCVMQ>("SELECT SaleMasterReturns.Id, SaleMasterReturns.Invid, SaleMasterReturns.Address, SaleMasterReturns.Phone, SaleMasterReturns.Date, SaleMasterReturns.CargoId, SaleMasterReturns.CargoCharges, SaleMasterReturns.NetAmount, SaleMasterReturns.DiscountAmount, SaleMasterReturns.GrandTotal, SaleMasterReturns.Total,  SaleMasterReturns.Vtype, SaleMasterReturns.Comid, SaleMasterReturns.AccountNo, Customers.Name FROM SaleMasterReturns INNER JOIN Customers ON SaleMasterReturns.AccountNo = Customers.AccountNo WHERE(SaleMasterReturns.Comid = '" + Session["Company"] + "') AND (SaleMasterReturns.Vtype = 'RSINVWTC') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SaleMasterReturns.Invid").ToList());
        }
        public ActionResult New(SaleMasterReturn SaleMasterReturn, Customer Customer, Cargo cargo)
        {
            SaleMasterReturn.Invid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Invid AS int)), 0) + 1 AS Invid FROM   SaleMasterReturns  WHERE (Comid = '" + Session["Company"] + "') and Vtype='RSINVWTC' ").FirstOrDefault();
            var ViewModel = new SaleWTCReturnVM
            {
                Customer = Customer,
                Cargo = cargo,
                SaleMasterReturn = SaleMasterReturn,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE  (status='Active')  and (Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Cargo_list = _context.Database.SqlQuery<Cargo>("SELECT *   FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "') ").ToList(),
                SaleRecent = _context.Database.SqlQuery<SaleWTCVMQ>("SELECT SaleMasterReturns.Id, SaleMasterReturns.Invid, SaleMasterReturns.Phone, SaleMasterReturns.Date, SaleMasterReturns.CargoId, SaleMasterReturns.CargoCharges, SaleMasterReturns.NetAmount, SaleMasterReturns.DiscountAmount, SaleMasterReturns.GrandTotal, SaleMasterReturns.Total,  SaleMasterReturns.Vtype, SaleMasterReturns.Comid, SaleMasterReturns.AccountNo,  Customers.Name FROM SaleMasterReturns INNER JOIN Customers ON SaleMasterReturns.AccountNo = Customers.AccountNo WHERE(SaleMasterReturns.Comid = '" + Session["Company"] + "') AND (SaleMasterReturns.Vtype = 'RSINVWTC') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SaleMasterReturns.Invid").ToList(),

            };
            return View(ViewModel);
        }
        public ActionResult Save(SaleMasterReturn SaleMasterReturn, TranscationDetail TranscationDetail, int[] prid, string[] productname, decimal[] saleprice, decimal[] qty, decimal[] nettotal, decimal[] ctn, decimal[] itemunit)
        {
            string varDirection = "";
            string AccountName = _context.Database.SqlQuery<string>("SELECT  AccountTitle  FROM ThirdLevels WHERE     (AccountNo = '" + SaleMasterReturn.AccountNo + "') and Comid= '" + Session["Company"] + "'").FirstOrDefault();
            string InvoiceMake = " Sale Return Parts To " + AccountName + " Against this  Invoice No:" + SaleMasterReturn.Invid + "";
            string Sale = " Sale Return Parts From " + AccountName + " Against this  Invoice No:" + SaleMasterReturn.Invid + "";
            string Stock = "Stock In : Sale Return Parts   For " + AccountName + "  Invoice No: " + SaleMasterReturn.Invid + "    ";
            if (SaleMasterReturn.Id == 0)
            {
                _context.tbl_SaleMasterReturn.Add(SaleMasterReturn);
                SaleMasterReturn.Comid = Convert.ToInt32(Session["Company"]);
                SaleMasterReturn.Vtype = "RSINVWTC";
                for (int i = 0; i < prid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO SaleDetailReturns (Invid, Itemid, ItemName, SP, Qty, NetTotal, Date, Vtype, Comid,ItemUnit,CTN)  VALUES ('" + SaleMasterReturn.Invid + "','" + prid[i] + "','" + productname[i] + "','" + saleprice[i] + "','" + qty[i] + "','" + nettotal[i] + "','" + SaleMasterReturn.Date + "','RSINVWTC','" + Session["Company"] + "','" + itemunit[i] + "','" + ctn[i] + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + SaleMasterReturn.Date + "','" + SaleMasterReturn.AccountNo + "','0','" + SaleMasterReturn.NetAmount + "','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Sale + "','" + SaleMasterReturn.Date + "',4400001,'" + SaleMasterReturn.NetAmount + "','0','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','CGS','" + SaleMasterReturn.Date + "','5500001','0','" + SaleMasterReturn.NetAmount + "','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Stock + "','" + SaleMasterReturn.Date + "',1100002,'" + SaleMasterReturn.NetAmount + "','0','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','0')");
                
                varDirection = "New";
                TempData["Reg"] = "Invoice Created Successfully";
            }
            else
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM SaleMasterReturns  WHERE (Vtype = 'RSINVWTC') AND (Invid = '" + SaleMasterReturn.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM SaleDetailReturns  WHERE (Vtype = 'RSINVWTC') AND (Invid = '" + SaleMasterReturn.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails  WHERE (Vtype in ('RSINVWTC')) AND (Invid = '" + SaleMasterReturn.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.tbl_SaleMasterReturn.Add(SaleMasterReturn);
                SaleMasterReturn.Comid = Convert.ToInt32(Session["Company"]);
                SaleMasterReturn.Vtype = "RSINVWTC";
                for (int i = 0; i < prid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO SaleDetailReturns (Invid, Itemid, ItemName, SP, Qty, NetTotal, Date, Vtype, Comid,ItemUnit,CTN)  VALUES ('" + SaleMasterReturn.Invid + "','" + prid[i] + "','" + productname[i] + "','" + saleprice[i] + "','" + qty[i] + "','" + nettotal[i] + "','" + SaleMasterReturn.Date + "','RSINVWTC','" + Session["Company"] + "','" + itemunit[i] + "','" + ctn[i] + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                //1
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
            "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + SaleMasterReturn.Date + "','" + SaleMasterReturn.AccountNo + "','0','" + SaleMasterReturn.NetAmount + "','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                //2
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + Sale + "','" + SaleMasterReturn.Date + "',4400001,'" + SaleMasterReturn.NetAmount + "','0','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','0')");
                //3
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
           "('" + TranscationDetail.Transid + "','CGS','" + SaleMasterReturn.Date + "','5500001','0','" + SaleMasterReturn.NetAmount + "','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','0')");
                //4
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + Stock + "','" + SaleMasterReturn.Date + "',1100002,'" + SaleMasterReturn.NetAmount + "','0','" + SaleMasterReturn.Invid + "','RSINVWTC','" + Session["Company"] + "','0')");

                varDirection = "Index";
                TempData["Reg"] = "Invoice Updated Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "SaleWTC");
        }
        public ActionResult Edit(int Invid, Customer Customer, Cargo cargo)
        {
            var ViewModel = new SaleWTCReturnVM
            {
                Customer = Customer,
                Cargo = cargo,
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Cargo_list = _context.Database.SqlQuery<Cargo>("SELECT *   FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "') ").ToList(),
                SaleDetailReturn_list = _context.Database.SqlQuery<SaleDetailReturn>("SELECT  * FROM   SaleDetailReturns  WHERE (Vtype='RSINVWTC')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                SaleMasterReturn = _context.Database.SqlQuery<SaleMasterReturn>("SELECT * FROM   SaleMasterReturns WHERE (Vtype = 'RSINVWTC') AND (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  (status='Active')  and  WHERE ( Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                SaleRecent = _context.Database.SqlQuery<SaleWTCVMQ>("SELECT SaleMasterReturns.Id, SaleMasterReturns.Invid, SaleMasterReturns.Address, SaleMasterReturns.Phone, SaleMasterReturns.Date, SaleMasterReturns.CargoId, SaleMasterReturns.CargoCharges, SaleMasterReturns.NetAmount, SaleMasterReturns.DiscountAmount, SaleMasterReturns.GrandTotal, SaleMasterReturns.Total, SaleMasterReturns.BTotal, SaleMasterReturns.Vtype, SaleMasterReturns.Comid, SaleMasterReturns.AccountNo, SaleMasterReturns.Rtotal, Customers.Name FROM SaleMasterReturns INNER JOIN Customers ON SaleMasterReturns.AccountNo = Customers.AccountNo WHERE(SaleMasterReturns.Comid = '" + Session["Company"] + "') AND (SaleMasterReturns.Vtype = 'RSINVWTC') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SaleMasterReturns.Invid").ToList(),
            };
            return View("New", ViewModel);
        }
        public ActionResult Print(int Invid, TranscationDetail TranscationDetail)
        {
            decimal NetAmount = _context.Database.SqlQuery<decimal>("SELECT NetAmount  FROM   SaleMasterReturns  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'RSINVWTC')").SingleOrDefault();
            var wordsinum = NumberToWords(Decimal.ToInt32(NetAmount));
            var ViewModel = new SaleWTCReturnVM
            {
                wordsinum = wordsinum,
                SaleDetailReturn_list = _context.Database.SqlQuery<SaleDetailReturn>("SELECT  * FROM   SaleDetailReturns  WHERE (Vtype='RSINVWTC')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PrintSale = _context.Database.SqlQuery<PrintSaleVMQ>("SELECT SaleMasterReturns.Id, SaleMasterReturns.Invid, SaleMasterReturns.Date, SaleMasterReturns.CargoId, SaleMasterReturns.CargoCharges, SaleMasterReturns.NetAmount, SaleMasterReturns.DiscountAmount, SaleMasterReturns.GrandTotal, SaleMasterReturns.Total,  SaleMasterReturns.Vtype, SaleMasterReturns.Comid, SaleMasterReturns.AccountNo,  Customers.Address, Customers.Email, Customers.Phone1, Customers.Name FROM SaleMasterReturns INNER JOIN Customers ON SaleMasterReturns.AccountNo = Customers.AccountNo WHERE(SaleMasterReturns.Vtype = 'RSINVWTC') AND (SaleMasterReturns.Comid = '" + Session["Company"] + "') AND (SaleMasterReturns.Invid = '" + Invid + "') AND (Customers.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE ( Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
            };
            return View("Print", ViewModel);
        }

        public ActionResult Action(int id)
        {
            var getfees = _context.Database.SqlQuery<Product>("SELECT *  FROM   Products WHERE (Id = '" + id + "')").ToList();
            return Json(getfees, JsonRequestBehavior.AllowGet);
        }
        private object NumberToWords(string number)
        {
            throw new NotImplementedException();
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            string new_word = Regex.Replace(words, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return new_word;
        }
        public ActionResult Save_cus(Customer Customer, string CNIC, ThirdLevel Thirdlevel, HttpPostedFileBase img)
        {
            string vardirection = "";
            string ImageName = "";
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
                Customer.CNIC = CNIC;
                Customer.AccountNo = account_no1;
                Customer.Image = ImageName;
                _context.tbl_Customer.Add(Customer);
                Customer.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("insert into Thirdlevels (AccountNo,FirstLevelId,HeadId,AccountTitle,AccountType,cr,dr,SecondLevelId,Comid) values(" + account_no1 + ",1001,1,N'" + Customer.Name + "','Customer',0,0,'1000002','" + Session["Company"] + "')");
                vardirection = "New";
                TempData["Reg1"] = "Registered Successfully";
            }

            return RedirectToAction(vardirection, "SaleWTC");
        }
        public ActionResult Save_cargo(Cargo Cargo)
        {
            string varDirection = "";

            _context.tbl_Cargo.Add(Cargo);
            Cargo.Comid = Convert.ToInt32(Session["Company"]);
            varDirection = "New";
            TempData["Reg1"] = "Registered Successfully";
            _context.SaveChanges();
            return RedirectToAction(varDirection, "SaleWTC");
        }
    }
}