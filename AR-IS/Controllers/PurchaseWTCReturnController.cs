using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class PurchaseWTCReturnController : Controller
    {
        private ApplicationDbContext _context;

        public PurchaseWTCReturnController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PurchaseWTCReturn
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<PurchaseWTCVMQ>("SELECT PurMasterReturns.Id, PurMasterReturns.Invid, PurMasterReturns.Address, PurMasterReturns.Phone, PurMasterReturns.Date, PurMasterReturns.CargoId, PurMasterReturns.CargoCharges, PurMasterReturns.NetAmount, PurMasterReturns.DiscountAmount, PurMasterReturns.GrandTotal, PurMasterReturns.Total,  PurMasterReturns.Vtype, PurMasterReturns.Comid, PurMasterReturns.AccountNo,  Suppliers.Name FROM PurMasterReturns INNER JOIN Suppliers ON PurMasterReturns.AccountNo = Suppliers.AccountNo WHERE(PurMasterReturns.Comid = '" + Session["Company"] + "') AND (PurMasterReturns.Vtype = 'RPINVWTC') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurMasterReturns.Invid").ToList());
        }

        public ActionResult New(PurMasterReturn PurMasterReturn, Supplier Supplier, Cargo cargo)
        {
            PurMasterReturn.Invid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Invid AS int)), 0) + 1 AS Invid FROM   PurMasterReturns  WHERE (Comid = '" + Session["Company"] + "') and Vtype='RPINVWTC' ").FirstOrDefault();
            var ViewModel = new PurchaseWTCReturnVM
            {
                Supplier = Supplier,
                Cargo = cargo,
                PurMasterReturn = PurMasterReturn,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE   (status='Active')  and  (Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Cargo_list = _context.Database.SqlQuery<Cargo>("SELECT *   FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "') ").ToList(),
                PurchaseRecent = _context.Database.SqlQuery<PurchaseWTCVMQ>("SELECT PurMasterReturns.Id, PurMasterReturns.Invid, PurMasterReturns.Address, PurMasterReturns.Phone, PurMasterReturns.Date, PurMasterReturns.CargoId, PurMasterReturns.CargoCharges, PurMasterReturns.NetAmount, PurMasterReturns.DiscountAmount, PurMasterReturns.GrandTotal, PurMasterReturns.Total, PurMasterReturns.Vtype, PurMasterReturns.Comid, PurMasterReturns.AccountNo, Suppliers.Name FROM PurMasterReturns INNER JOIN Suppliers ON PurMasterReturns.AccountNo = Suppliers.AccountNo WHERE(PurMasterReturns.Comid = '" + Session["Company"] + "') AND (PurMasterReturns.Vtype = 'RPINVWTC') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurMasterReturns.Invid").ToList(),
            };
            return View(ViewModel);
        }
        public ActionResult Save(PurMasterReturn PurMasterReturn, TranscationDetail TranscationDetail, int[] prid, string[] productname, decimal[] costprice, decimal[] qty, decimal[] nettotal, decimal[] ctn, decimal[] itemunit)
        {
            string varDirection = "";
            string AccountName = _context.Database.SqlQuery<string>("SELECT  AccountTitle  FROM ThirdLevels WHERE     (AccountNo = '" + PurMasterReturn.AccountNo + "') and Comid= '" + Session["Company"] + "'").FirstOrDefault();
            string InvoiceMake = " Purchase Return Parts To " + AccountName + " Against this  Invoice No:" + PurMasterReturn.Invid + "";
            string Stock = "Stock Out :   Purchase  Return Parts  To   " + AccountName + "  Invoice No: " + PurMasterReturn.Invid + "    ";
          
            if (PurMasterReturn.Id == 0)
            {
                _context.tbl_PurMasterReturn.Add(PurMasterReturn);
                PurMasterReturn.Comid = Convert.ToInt32(Session["Company"]);
                PurMasterReturn.Vtype = "RPINVWTC";
                for (int i = 0; i < prid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO PurDetailReturns (Invid, Itemid, ItemName, CP, Qty, NetTotal, Date, Vtype, Comid,ItemUnit,CTN,AccountNo)  VALUES ('" + PurMasterReturn.Invid + "','" + prid[i] + "','" + productname[i] + "','" + costprice[i] + "','" + qty[i] + "','" + nettotal[i] + "','" + PurMasterReturn.Date + "','RPINVWTC','" + Session["Company"] + "','" + itemunit[i] + "','" + ctn[i] + "','" + PurMasterReturn.AccountNo + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + PurMasterReturn.Date + "','" + PurMasterReturn.AccountNo + "','" + PurMasterReturn.NetAmount + "','0','" + PurMasterReturn.Invid + "','RPINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Stock + "','" + PurMasterReturn.Date + "',1100002,'0','" + PurMasterReturn.NetAmount + "','" + PurMasterReturn.Invid + "','RPINVWTC','" + Session["Company"] + "','0')");
                
                varDirection = "New";
                TempData["Reg"] = "Invoice Created Successfully";
            }
            else
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM PurMasterReturns  WHERE (Vtype = 'RPINVWTC') AND (Invid = '" + PurMasterReturn.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM PurDetailReturns  WHERE (Vtype = 'RPINVWTC') AND (Invid = '" + PurMasterReturn.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails  WHERE (Vtype in ('RPINVWTC') AND (Invid = '" + PurMasterReturn.Invid + "') AND (Comid = '" + Session["Company"] + "')) ");
                _context.tbl_PurMasterReturn.Add(PurMasterReturn);
                PurMasterReturn.Comid = Convert.ToInt32(Session["Company"]);
                PurMasterReturn.Vtype = "RPINVWTC";
                for (int i = 0; i < prid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO PurDetailReturns (Invid, Itemid, ItemName, CP, Qty, NetTotal, Date, Vtype, Comid,ItemUnit,CTN,AccountNo)  VALUES ('" + PurMasterReturn.Invid + "','" + prid[i] + "','" + productname[i] + "','" + costprice[i] + "','" + qty[i] + "','" + nettotal[i] + "','" + PurMasterReturn.Date + "','RPINVWTC','" + Session["Company"] + "','" + itemunit[i] + "','" + ctn[i] + "','" + PurMasterReturn.AccountNo + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
            "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + PurMasterReturn.Date + "','" + PurMasterReturn.AccountNo + "','" + PurMasterReturn.NetAmount + "','0','" + PurMasterReturn.Invid + "','RPINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + Stock + "','" + PurMasterReturn.Date + "',1100002,'0','" + PurMasterReturn.NetAmount + "','" + PurMasterReturn.Invid + "','RPINVWTC','" + Session["Company"] + "','0')");

                varDirection = "Index";
                TempData["Reg"] = "Invoice Updated Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "PurchaseWTCReturn");
        }
        public ActionResult Edit(int Invid, Supplier Supplier, Cargo cargo)
        {
            var ViewModel = new PurchaseWTCReturnVM
            {
                Supplier = Supplier,
                Cargo = cargo,
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Cargo_list = _context.Database.SqlQuery<Cargo>("SELECT *   FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "') ").ToList(),
                PurDetailReturn_list = _context.Database.SqlQuery<PurDetailReturn>("SELECT  * FROM   PurDetailReturns  WHERE (Vtype='RPINVWTC')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PurMasterReturn = _context.Database.SqlQuery<PurMasterReturn>("SELECT * FROM   PurMasterReturns WHERE (Vtype = 'RPINVWTC') AND (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE    (status='Active')  and ( Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                PurchaseRecent = _context.Database.SqlQuery<PurchaseWTCVMQ>("SELECT PurMasterReturns.Id, PurMasterReturns.Invid, PurMasterReturns.Address, PurMasterReturns.Phone, PurMasterReturns.Date, PurMasterReturns.CargoId, PurMasterReturns.CargoCharges, PurMasterReturns.NetAmount, PurMasterReturns.DiscountAmount, PurMasterReturns.GrandTotal, PurMasterReturns.Total, PurMasterReturns.Vtype, PurMasterReturns.Comid, PurMasterReturns.AccountNo, Suppliers.Name FROM PurMasterReturns INNER JOIN Suppliers ON PurMasterReturns.AccountNo = Suppliers.AccountNo WHERE(PurMasterReturns.Comid = '" + Session["Company"] + "') AND (PurMasterReturns.Vtype = 'RPINVWTC') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurMasterReturns.Invid").ToList(),
            };
            return View("New", ViewModel);
        }
        public ActionResult Print(int Invid, TranscationDetail TranscationDetail)
        {
            decimal NetAmount = _context.Database.SqlQuery<decimal>("SELECT NetAmount  FROM   PurMasterReturns  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'RPINVWTC')").SingleOrDefault();
            var wordsinum = NumberToWords(Decimal.ToInt32(NetAmount));
            var ViewModel = new PurchaseWTCReturnVM
            {
                wordsinum = wordsinum,
                PurDetailReturn_list = _context.Database.SqlQuery<PurDetailReturn>("SELECT  * FROM   PurDetailReturns  WHERE (Vtype='RPINVWTC')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PrintPurchase = _context.Database.SqlQuery<PrintPurchaseVMQ>("SELECT PurMasterReturns.Id, PurMasterReturns.Invid, PurMasterReturns.Date, PurMasterReturns.CargoId, PurMasterReturns.CargoCharges, PurMasterReturns.NetAmount, PurMasterReturns.DiscountAmount, PurMasterReturns.GrandTotal, PurMasterReturns.Total,  PurMasterReturns.Vtype, PurMasterReturns.Comid, PurMasterReturns.AccountNo,  Suppliers.Address, Suppliers.Email, Suppliers.Phone, Suppliers.Name FROM PurMasterReturns INNER JOIN Suppliers ON PurMasterReturns.AccountNo = Suppliers.AccountNo WHERE(PurMasterReturns.Vtype = 'RPINVWTC') AND (PurMasterReturns.Comid = '" + Session["Company"] + "') AND (PurMasterReturns.Invid = '" + Invid + "') AND (Suppliers.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
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
        public ActionResult Save_sup(Supplier Supplier, string CNIC, ThirdLevel Thirdlevel, HttpPostedFileBase img)
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
                Supplier.CNIC = CNIC;
                Supplier.AccountNo = account_no1;
                Supplier.Image = ImageName;
                _context.tbl_Supplier.Add(Supplier);
                Supplier.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("insert into Thirdlevels (AccountNo,FirstLevelId,HeadId,AccountTitle,AccountType,Cr,Dr,SecondLevelId,Comid) values(" + account_no1 + ",2001,2,N'" + Supplier.Name + "','Supplier',0,0,'2000001','" + Session["Company"] + "')");
                vardirection = "New";
                TempData["Reg1"] = "Registered Successfully";
            }

            return RedirectToAction(vardirection, "PurchaseWTC");
        }
        public ActionResult Save_cargo(Cargo Cargo)
        {
            string varDirection = "";

            _context.tbl_Cargo.Add(Cargo);
            Cargo.Comid = Convert.ToInt32(Session["Company"]);
            varDirection = "New";
            TempData["Reg1"] = "Registered Successfully";


            _context.SaveChanges();
            return RedirectToAction(varDirection, "PurchaseWTC");
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
    }
}