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
    public class PurchaseVehicleController : Controller
    {
        private ApplicationDbContext _context;

        public PurchaseVehicleController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PurchaseVehicle
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<PurchaseWVehicleVMQ>("SELECT PurMasterVehicles.Id, PurMasterVehicles.Invid, PurMasterVehicles.Address, PurMasterVehicles.Phone, PurMasterVehicles.Date, PurMasterVehicles.CargoCharges, PurMasterVehicles.NetAmount, PurMasterVehicles.GrandTotal, PurMasterVehicles.Total, PurMasterVehicles.Ptotal, PurMasterVehicles.BTotal, PurMasterVehicles.Vtype, PurMasterVehicles.Comid, Suppliers.Name, PurMasterVehicles.AccountNo FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE(PurMasterVehicles.Vtype = 'PINVV') AND (PurMasterVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(PurMasterVehicle PurMasterVehicle, Supplier Supplier, Cargo cargo)
        {
            PurMasterVehicle.Invid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Invid AS int)), 0) + 1 AS Invid FROM   PurDetailVehicles  WHERE (Comid = '" + Session["Company"] + "') and Vtype='PINVV'").FirstOrDefault();
            var ViewModel = new PurchaseVehicleVM
            {
                Cargo = cargo,
                PurMasterVehicle = PurMasterVehicle,
                Supplier = Supplier,
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                Cargo_list = _context.Database.SqlQuery<Cargo>("SELECT *   FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "') ").ToList(),
                PurchaseRecent =_context.Database.SqlQuery<PurchaseWVehicleVMQ>("SELECT PurMasterVehicles.Id, PurMasterVehicles.Invid, PurMasterVehicles.Address, PurMasterVehicles.Phone, PurMasterVehicles.Date, PurMasterVehicles.CargoCharges, PurMasterVehicles.NetAmount, PurMasterVehicles.GrandTotal, PurMasterVehicles.Total, PurMasterVehicles.Ptotal, PurMasterVehicles.BTotal, PurMasterVehicles.Vtype, PurMasterVehicles.Comid, Suppliers.Name, PurMasterVehicles.AccountNo FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE(PurMasterVehicles.Vtype = 'PINVV') AND (PurMasterVehicles.Comid = '" + Session["Company"] + "')").ToList(),
             };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Save(PurMasterVehicle PurMasterVehicle,PurDetailVehicle PurDetailVehicle, TranscationDetail TranscationDetail ,string [] vname ,string [] modelno ,string [] KeyNo, string [] Chassisno,string [] engineno,string [] color ,string [] Remarks, decimal [] vrate ,decimal [] gst,decimal [] withoutgst ,decimal [] withgst,decimal [] Discount)
        {
            string varDirection = "";
            string AccountName = _context.Database.SqlQuery<string>("SELECT  AccountTitle  FROM ThirdLevels WHERE     (AccountNo = '" + PurMasterVehicle.AccountNo + "') and Comid= '" + Session["Company"] + "'").FirstOrDefault();
            string InvoiceMake = " Purchase Vehicle From " + AccountName + " Against this  Invoice No: "+PurMasterVehicle.Invid + "";
            string Stock = "Stock In : Vehicle Purchase   From " + AccountName + "  Invoice No: " + PurMasterVehicle.Invid + "    ";
            string Paid = " Paid To " + AccountName + " Against this Purchase Vehicle Invoice No: " + PurMasterVehicle.Invid + " ";
            if (PurMasterVehicle.Id == 0)
            {
                _context.tbl_PurMasterVehicle.Add(PurMasterVehicle);
                PurMasterVehicle.Comid = Convert.ToInt32(Session["Company"]);
                PurMasterVehicle.Vtype = "PINVV";
                for (int i = 0; i < vname.Count(); i++)
                {

                    PurDetailVehicle.Vid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Vid AS int)), 0) + 1 AS Vid FROM   PurDetailVehicles  WHERE (Comid = '" + Session["Company"] + "') and Vtype='PINVV'").FirstOrDefault();

                    string MergingId = "" + engineno[i] + "-" + PurDetailVehicle.Vid + "  ";


                    _context.Database.ExecuteSqlCommand("INSERT   INTO     PurDetailVehicles( Invid, VehicleName, ModelNo, ChassiNo, EngineNo, Color, Remarks, Date, Vtype, Comid, Rate, GST, WithoutGSTTotal, WithGSTTotal,KeyNo,Discount,AdvancePayment,BalanceTotal,AccountNo,Vid,MergingId)  " +
                        "VALUES ('"+ PurMasterVehicle.Invid + "','" + vname[i] + "','" + modelno[i] + "','" + Chassisno[i] + "','" + engineno[i] + "','" + color[i] + "','" + Remarks[i] + "','" + PurMasterVehicle.Date + "','PINVV','" + Session["Company"] + "','"+vrate[i]+ "','" + gst[i] + "','"+ withoutgst[i] + "','"+ withgst[i]+ "','"+KeyNo[i]+ "','" + Discount[i] + "','0','0','"+ PurMasterVehicle.AccountNo+ "','"+ PurDetailVehicle.Vid+ "','" + MergingId + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (PurMasterVehicle.Ptotal == 0)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','"+ InvoiceMake + "','" + PurMasterVehicle.Date + "','" + PurMasterVehicle.AccountNo + "','0','" + PurMasterVehicle.NetAmount + "','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Stock + "','" + PurMasterVehicle.Date + "',1100002,'" + PurMasterVehicle.NetAmount + "','0','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + PurMasterVehicle.Date + "','" + PurMasterVehicle.AccountNo + "','0','" + PurMasterVehicle.NetAmount + "','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Stock + "','" + PurMasterVehicle.Date + "',1100002,'" + PurMasterVehicle.NetAmount + "','0','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Paid + "','" + PurMasterVehicle.Date + "','" + PurMasterVehicle.AccountNo + "','" + PurMasterVehicle.Ptotal + "','0','" + PurMasterVehicle.Invid + "','CPVPINVV','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                        "('" + TranscationDetail.Transid + "','" + Paid + "','" + PurMasterVehicle.Date + "',1100001,'0','" + PurMasterVehicle.Ptotal + "','" + PurMasterVehicle.Invid + "','CPVPINVV','" + Session["Company"] + "','0')");
                }
                varDirection = "New";
                TempData["Reg"] = "Invoice Created Successfully";
            }
            else
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM PurMasterVehicles  WHERE (Vtype = 'PINVV') AND (Invid = '" + PurMasterVehicle.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM PurDetailVehicles  WHERE (Vtype = 'PINVV') AND (Invid = '" + PurMasterVehicle.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails  WHERE (Vtype in ('PINVV','CPVPINVV') AND (Invid = '" + PurMasterVehicle.Invid + "') AND (Comid = '" + Session["Company"] + "')) ");
                _context.tbl_PurMasterVehicle.Add(PurMasterVehicle);
                PurMasterVehicle.Comid = Convert.ToInt32(Session["Company"]);
                PurMasterVehicle.Vtype = "PINVV";
                for (int i = 0; i < vname.Count(); i++)
                {
                    PurDetailVehicle.Vid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Vid AS int)), 0) + 1 AS Vid FROM   PurDetailVehicles  WHERE (Comid = '" + Session["Company"] + "') and Vtype='PINVV'").FirstOrDefault();
                    string MergingId = "" + engineno[i] + "-" + PurDetailVehicle.Vid + "  ";
                    _context.Database.ExecuteSqlCommand("INSERT   INTO     PurDetailVehicles( Invid, VehicleName, ModelNo, ChassiNo, EngineNo, Color, Remarks, Date, Vtype, Comid, Rate, GST, WithoutGSTTotal, WithGSTTotal,KeyNo,Discount,AdvancePayment,BalanceTotal,AccountNo,Vid,MergingId)  " +
                        "VALUES ('" + PurMasterVehicle.Invid + "','" + vname[i] + "','" + modelno[i] + "','" + Chassisno[i] + "','" + engineno[i] + "','" + color[i] + "','" + Remarks[i] + "','" + PurMasterVehicle.Date + "','PINVV','" + Session["Company"] + "','" + vrate[i] + "','" + gst[i] + "','" + withoutgst[i] + "','" + withgst[i] + "','" + KeyNo[i] + "','" + Discount[i] + "','0','0','" + PurMasterVehicle.AccountNo + "','" + PurDetailVehicle.Vid + "','" + MergingId + "')");
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (PurMasterVehicle.Ptotal == 0)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + PurMasterVehicle.Date + "','" + PurMasterVehicle.AccountNo + "','0','" + PurMasterVehicle.NetAmount + "','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Stock + "','" + PurMasterVehicle.Date + "',1100002,'" + PurMasterVehicle.NetAmount + "','0','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + InvoiceMake + "','" + PurMasterVehicle.Date + "','" + PurMasterVehicle.AccountNo + "','0','" + PurMasterVehicle.NetAmount + "','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Stock + "','" + PurMasterVehicle.Date + "',1100002,'" + PurMasterVehicle.NetAmount + "','0','" + PurMasterVehicle.Invid + "','PINVV','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Paid + "','" + PurMasterVehicle.Date + "','" + PurMasterVehicle.AccountNo + "','" + PurMasterVehicle.Ptotal + "','0','" + PurMasterVehicle.Invid + "','CPVPINVV','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                        "('" + TranscationDetail.Transid + "','" + Paid + "','" + PurMasterVehicle.Date + "',1100001,'0','" + PurMasterVehicle.Ptotal + "','" + PurMasterVehicle.Invid + "','CPVPINVV','" + Session["Company"] + "','0')");
                }
                varDirection = "Index";
                TempData["Reg"] = "Invoice Updated Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "PurchaseVehicle");
        }
        public ActionResult Edit(int Invid, TranscationDetail TranscationDetail,Supplier Supplier, Cargo  cargo)
        {
            var ViewModel = new PurchaseVehicleVM
            {
                Cargo = cargo,
                Supplier = Supplier,
                Cargo_list = _context.Database.SqlQuery<Cargo>("SELECT *   FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "') ").ToList(),
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                PurDetailVehicle_list = _context.Database.SqlQuery<PurDetailVehicle>("SELECT  * FROM   PurDetailVehicles  WHERE (Vtype='PINVV')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PurMasterVehicle = _context.Database.SqlQuery<PurMasterVehicle>("SELECT  * FROM   PurMasterVehicles  WHERE (Vtype='PINVV')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                PurchaseRecent = _context.Database.SqlQuery<PurchaseWVehicleVMQ>("SELECT PurMasterVehicles.Id, PurMasterVehicles.Invid, PurMasterVehicles.Address, PurMasterVehicles.Phone, PurMasterVehicles.Date, PurMasterVehicles.CargoCharges, PurMasterVehicles.NetAmount, PurMasterVehicles.GrandTotal, PurMasterVehicles.Total, PurMasterVehicles.Ptotal, PurMasterVehicles.BTotal, PurMasterVehicles.Vtype, PurMasterVehicles.Comid, Suppliers.Name, PurMasterVehicles.AccountNo FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE(PurMasterVehicles.Vtype = 'PINVV') AND (PurMasterVehicles.Comid = '" + Session["Company"] + "')").ToList(),
            };
            return View("New", ViewModel);
        }
        public ActionResult Print(int Invid, TranscationDetail TranscationDetail)
        {
            decimal NetAmount = _context.Database.SqlQuery<decimal>("SELECT NetAmount  FROM   PurMasterVehicles  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'PINVV')").SingleOrDefault();
            var wordsinum = NumberToWords(Decimal.ToInt32(NetAmount));
            var ViewModel = new PurchaseVehicleVM
            {
                wordsinum = wordsinum,
                PurDetailVehicle_list = _context.Database.SqlQuery<PurDetailVehicle>("SELECT  * FROM   PurDetailVehicles  WHERE (Vtype='PINVV')  AND  (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").ToList(),
                PrintPurchase = _context.Database.SqlQuery<PrintPurchaseVehicleVMQ>("SELECT PurMasterVehicles.Id, PurMasterVehicles.Invid, PurMasterVehicles.Date, PurMasterVehicles.CargoCharges, PurMasterVehicles.NetAmount, PurMasterVehicles.GrandTotal, PurMasterVehicles.Total, PurMasterVehicles.BTotal, PurMasterVehicles.Vtype, PurMasterVehicles.Comid, PurMasterVehicles.AccountNo, PurMasterVehicles.Ptotal, Suppliers.Address, Suppliers.Email, Suppliers.Phone, Suppliers.Name FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE(PurMasterVehicles.Vtype = 'PINVV') AND (PurMasterVehicles.Comid = '" + Session["Company"] + "') AND (PurMasterVehicles.Invid = '" + Invid + "') AND (Suppliers.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
            };
            return View("Print", ViewModel);
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
            
            return RedirectToAction(vardirection, "PurchaseVehicle");
        }

        public ActionResult Save_cargo(Cargo Cargo)
        {
            string varDirection = "";

            _context.tbl_Cargo.Add(Cargo);
            Cargo.Comid = Convert.ToInt32(Session["Company"]);
            varDirection = "New";
            TempData["Reg1"] = "Registered Successfully";
            _context.SaveChanges();
            return RedirectToAction(varDirection, "PurchaseVehicle");
        }
    }
}