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
    public class PurchaseWTCController : Controller
    {
        private ApplicationDbContext _context;

        public PurchaseWTCController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PurchaseWTC
       
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<PurchaseWTCVMQ>("SELECT PurMasters.Id, PurMasters.Invid, PurMasters.Address, PurMasters.Phone, PurMasters.Date, PurMasters.CargoName, PurMasters.CargoCharges, PurMasters.NetAmount, PurMasters.DiscountAmount, PurMasters.GrandTotal, PurMasters.Total, PurMasters.BTotal, PurMasters.Vtype, PurMasters.Comid, PurMasters.AccountNo, PurMasters.Ptotal, Suppliers.Name FROM PurMasters INNER JOIN Suppliers ON PurMasters.AccountNo = Suppliers.AccountNo WHERE(PurMasters.Comid = '" + Session["Company"] + "') AND (PurMasters.Vtype = 'PINVWTC') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurMasters.Invid").ToList());
        }
        public ActionResult New(PurMaster PurMaster, TranscationDetail TranscationDetail)
        {
            PurMaster.Invid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Invid AS int)), 0) + 1 AS Invid FROM   PurMasters  WHERE (Comid = '" + Session["Company"] + "') and Vtype='PINVWTC' ").FirstOrDefault();
            var ViewModel = new PurchaseWTCVM
            {
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE (Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                PurMaster = PurMaster,
                TranscationDetail = TranscationDetail,
                PurchaseRecent= _context.Database.SqlQuery<PurchaseWTCVMQ>("SELECT PurMasters.Id, PurMasters.Invid, PurMasters.Address, PurMasters.Phone, PurMasters.Date, PurMasters.CargoName, PurMasters.CargoCharges, PurMasters.NetAmount, PurMasters.DiscountAmount, PurMasters.GrandTotal, PurMasters.Total, PurMasters.BTotal, PurMasters.Vtype, PurMasters.Comid, PurMasters.AccountNo, PurMasters.Ptotal, Suppliers.Name FROM PurMasters INNER JOIN Suppliers ON PurMasters.AccountNo = Suppliers.AccountNo WHERE(PurMasters.Comid = '" + Session["Company"] + "') AND (PurMasters.Vtype = 'PINVWTC') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurMasters.Invid").ToList(),
            };
            return View(ViewModel);
        }
        public ActionResult Save(PurMaster PurMaster, TranscationDetail TranscationDetail, int[] prid, string[] productname, decimal[] costprice, decimal[] qty, decimal[] nettotal, decimal[] ctn, decimal[] itemunit)
        {
            string varDirection = "";
            if (PurMaster.Id == 0)
            {
                _context.tbl_PurMaster.Add(PurMaster);
                PurMaster.Comid = Convert.ToInt32(Session["Company"]);
                PurMaster.Vtype = "PINVWTC";
                for (int i = 0; i < prid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO PurDetails (Invid, Itemid, ItemName, CP, Qty, NetTotal, Date, Vtype, Comid,ItemUnit,CTN)  VALUES ('" + PurMaster.Invid + "','" + prid[i] + "','" + productname[i] + "','" + costprice[i] + "','" + qty[i] + "','" + nettotal[i] + "','" + PurMaster.Date + "','PINVWTC','" + Session["Company"] + "','" + itemunit[i] + "','" + ctn[i] + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (PurMaster.Ptotal == 0)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurMaster.Date + "','" + PurMaster.AccountNo + "','0','" + PurMaster.NetAmount + "','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurMaster.Date + "',1100002,'" + PurMaster.NetAmount + "','0','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurMaster.Date + "','" + PurMaster.AccountNo + "','0','" + PurMaster.NetAmount + "','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurMaster.Date + "',1100002,'" + PurMaster.NetAmount + "','0','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Supplier','" + PurMaster.Date + "','" + PurMaster.AccountNo + "','" + PurMaster.Ptotal + "','0','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                        "('" + TranscationDetail.Transid + "','Cash','" + PurMaster.Date + "',1100001,'0','" + PurMaster.Ptotal + "','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                }
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM PurMasters  WHERE (Vtype = 'PINVWTC') AND (Invid = '" + PurMaster.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM PurDetails  WHERE (Vtype = 'PINVWTC') AND (Invid = '" + PurMaster.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails  WHERE (Vtype = 'PINVWTC') AND (Invid = '" + PurMaster.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.tbl_PurMaster.Add(PurMaster);
                PurMaster.Comid = Convert.ToInt32(Session["Company"]);
                PurMaster.Vtype = "PINVWTC";
                for (int i = 0; i < prid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO PurDetails (Invid, Itemid, ItemName, CP, Qty, NetTotal, Date, Vtype, Comid,ItemUnit,CTN)  VALUES ('" + PurMaster.Invid + "','" + prid[i] + "','" + productname[i] + "','" + costprice[i] + "','" + qty[i] + "','" + nettotal[i] + "','" + PurMaster.Date + "','PINVWTC','" + Session["Company"] + "','" + itemunit[i] + "','" + ctn[i] + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (PurMaster.Ptotal == 0)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurMaster.Date + "','" + PurMaster.AccountNo + "','0','" + PurMaster.NetAmount + "','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurMaster.Date + "',1100002,'" + PurMaster.NetAmount + "','0','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurMaster.Date + "','" + PurMaster.AccountNo + "','0','" + PurMaster.NetAmount + "','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurMaster.Date + "',1100002,'" + PurMaster.NetAmount + "','0','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Supplier','" + PurMaster.Date + "','" + PurMaster.AccountNo + "','" + PurMaster.Ptotal + "','0','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                        "('" + TranscationDetail.Transid + "','Cash','" + PurMaster.Date + "',1100001,'0','" + PurMaster.Ptotal + "','" + PurMaster.Invid + "','PINVWTC','" + Session["Company"] + "','0')");
                }
                varDirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "PurchaseWTC");
        }
        public ActionResult Edit(int Invid, TranscationDetail TranscationDetail)
        {
            var ViewModel = new PurchaseWTCVM
            {
                PurDetail_list = _context.Database.SqlQuery<PurDetail>("SELECT  * FROM   PurDetails  WHERE (Vtype='PINVWTC')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PurMaster = _context.Database.SqlQuery<PurMaster>("SELECT * FROM   PurMasters WHERE (Vtype = 'PINVWTC') AND (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE ( Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                TranscationDetail = _context.Database.SqlQuery<TranscationDetail>("SELECT  *    FROM   TranscationDetails  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'PINVWTC') AND (TransDes = 'Supplier')").FirstOrDefault(),
                PurchaseRecent = _context.Database.SqlQuery<PurchaseWTCVMQ>("SELECT PurMasters.Id, PurMasters.Invid, PurMasters.Address, PurMasters.Phone, PurMasters.Date, PurMasters.CargoName, PurMasters.CargoCharges, PurMasters.NetAmount, PurMasters.DiscountAmount, PurMasters.GrandTotal, PurMasters.Total, PurMasters.BTotal, PurMasters.Vtype, PurMasters.Comid, PurMasters.AccountNo, PurMasters.Ptotal, Suppliers.Name FROM PurMasters INNER JOIN Suppliers ON PurMasters.AccountNo = Suppliers.AccountNo WHERE(PurMasters.Comid = '" + Session["Company"] + "') AND (PurMasters.Vtype = 'PINVWTC') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurMasters.Invid").ToList(),
            };
            return View("New", ViewModel);
        }
        public ActionResult Print(int Invid, TranscationDetail TranscationDetail)
        {
            decimal NetAmount = _context.Database.SqlQuery<decimal>("SELECT NetAmount  FROM   PurMasters  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'PINVWTC')").SingleOrDefault();
            var wordsinum = NumberToWords(Decimal.ToInt32(NetAmount));
            var ViewModel = new PurchaseWTCVM
            {
                wordsinum= wordsinum,
                PurDetail_list = _context.Database.SqlQuery<PurDetail>("SELECT  * FROM   PurDetails  WHERE (Vtype='PINVWTC')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PrintPurchase = _context.Database.SqlQuery<PrintPurchaseVMQ>("SELECT PurMasters.Id, PurMasters.Invid, PurMasters.Date, PurMasters.CargoName, PurMasters.CargoCharges, PurMasters.NetAmount, PurMasters.DiscountAmount, PurMasters.GrandTotal, PurMasters.Total, PurMasters.BTotal, PurMasters.Vtype, PurMasters.Comid, PurMasters.AccountNo, PurMasters.Ptotal, Suppliers.Address, Suppliers.Email, Suppliers.Phone, Suppliers.Name FROM PurMasters INNER JOIN Suppliers ON PurMasters.AccountNo = Suppliers.AccountNo WHERE(PurMasters.Vtype = 'PINVWTC') AND (PurMasters.Comid = '" + Session["Company"] + "') AND (PurMasters.Invid = '" + Invid + "') AND (Suppliers.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Prod_list = _context.Database.SqlQuery<Product>("SELECT *   FROM   Products  WHERE ( Comid = '" + Session["Company"] + "')  ORDER BY Id").ToList(),
                TranscationDetail = _context.Database.SqlQuery<TranscationDetail>("SELECT  *    FROM   TranscationDetails  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'PINVWTC') AND (TransDes = 'Supplier')").FirstOrDefault(),
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
    }
}