using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult New(GeneralUser  GeneralUser)
        {
            var ViewModel = new LoginVM
            {
                GeneralUser = GeneralUser,
            };
            return View(ViewModel);
        }
        public ActionResult ForgotPassword()
        {
            
            return View();
        }
        [HttpPost]
       
        public ActionResult Save(GeneralUser GeneralUser)
        {
            int COUNT = _context.Database.SqlQuery<int>("SELECT   COUNT(UserName) AS Expr1  FROM    GeneralUsers   WHERE   (UserName = '" + GeneralUser.UserName + "')").FirstOrDefault();
            if(COUNT==1)
            {
                var ViewModel = new LoginVM
                {
                    GeneralUser = GeneralUser,
                };
                return View("New", ViewModel);
            }
            else
            {
                 int Comid = _context.Database.SqlQuery<int>("SELECT  ISNULL(MAX(CAST(Comid AS int)), 0) + 1 AS Comid  FROM    Companies").FirstOrDefault();
                _context.Database.ExecuteSqlCommand("INSERT   INTO  Companies( Name, Comid)   VALUES ('Demo','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO  Settings( Companyname, Comid)  VALUES ('Demo','" + Comid + "') ");
                //Firstlevel
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Firstlevels( AccountNo, Headid, AccountTitle, Comid)  VALUES  (1001,1,'Current Assets','" + Comid + "')  ");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Firstlevels( AccountNo, Headid, AccountTitle, Comid)  VALUES  (2001,2,'Current liability','" + Comid + "')  ");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Firstlevels( AccountNo, Headid, AccountTitle, Comid)  VALUES  (4001,4,'Sale','" + Comid + "')  ");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Firstlevels( AccountNo, Headid, AccountTitle, Comid)  VALUES  (5001,5,'Expense','" + Comid + "')  ");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Firstlevels( AccountNo, Headid, AccountTitle, Comid)  VALUES  (5002,5,'Expense Sheet','" + Comid + "')  ");
                //second level
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (1,1001,1000001,'Cash','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (1,1001,1000002,'Customer','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (2,2001,2000001,'Supplier','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (4,4001,4000001,'Sale','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (1,1001,1000003,'Bank','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (1,1001,1000004,'Stock','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (5,5001,5000001,'CGS','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (5,5001,5000002,'Discount','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (5,5001,5000003,'Kitchen ','" + Comid + "')");
                _context.Database.ExecuteSqlCommand("INSERT  INTO   Secondlevels( Headid, SubHeadid, AccountNo, AccountTitle, Comid)  VALUES (5,5001,5000004,'MISC','" + Comid + "')");
                //Third Level
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (1100001,1,1001,1000001,'Cash','Cash',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (4400001,4,4001,4000001,'Sales','Sale',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (5500001,5,5001,5000001,'CGS','CGS',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (5500002,5,5001,5000002,'Discount','Discount',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (1100002,1,1001,1000004,'Stock','Stock',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (5500003,5,5002,5000003,'Food','Kitchen',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (5500004,5,5002,5000004,'Guests','MISC',0,0,'" + Comid + "') ");
                _context.Database.ExecuteSqlCommand("INSERT INTO  ThirdLevels( AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Cr, Dr, Comid)  VALUES  (5500005,5,5002,5000003,'Tea','Kitchen',0,0,'" + Comid + "') ");
                //string ab = "";
                //string dsa = GeneralUser.Password;
                //using (SHA1Managed sha1 = new SHA1Managed())
                //{
                //    var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(dsa));
                //    // declare stringbuilder
                //    var sb = new StringBuilder(hashSh1.Length * 2);

                //    // computing hashSh1
                //    foreach (byte b in hashSh1)
                //    {
                //        // "x2"
                //        sb.Append(b.ToString("X2").ToLower());
                //    }

                //    // final output
                //    Console.WriteLine(string.Format("The SHA1 hash of {0} is: {1}", dsa, sb.ToString()));
                //    ab = sb.ToString();
                //}
                GeneralUser.Password = GeneralUser.Password;
                DateTime now = DateTime.Now;
                GeneralUser.RegDate = now;
                GeneralUser.ExpDate = now.AddDays(13);
                GeneralUser.Comid = Comid;
                _context.tbl_GeneralUser.Add(GeneralUser);
                _context.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
        }
       
        public ActionResult Login(GeneralUser GeneralUser)
        {
            var Companies = _context.tbl_Company.ToList();
            var alert = "0";
            var viewmodel = new LoginVM
            {
               
                alert = alert,
                GeneralUser = GeneralUser,
                Companies = Companies
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        
        public ActionResult Login_check(GeneralUser GeneralUser)
        {
            //string ab = "";
            //string dsa = GeneralUser.Password;
            //using (SHA1Managed sha1 = new SHA1Managed())
            //{
            //    var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(dsa));
            //    // declare stringbuilder
            //    var sb = new StringBuilder(hashSh1.Length * 2);

            //    // computing hashSh1
            //    foreach (byte b in hashSh1)
            //    {
            //        // "x2"
            //        sb.Append(b.ToString("X2").ToLower());
            //    }

            //    // final output
            //    Console.WriteLine(string.Format("The SHA1 hash of {0} is: {1}", dsa, sb.ToString()));
            //    ab = sb.ToString();
            //}

            if (ModelState.IsValid)
            {
                var obj = _context.tbl_GeneralUser.Where(a => a.UserName.Equals(GeneralUser.UserName) && a.Password.Equals(GeneralUser.Password) && a.ExpDate > DateTime.Now).FirstOrDefault();
                if (obj != null)
                {
                    int total_days = (obj.ExpDate.AddDays(1) - DateTime.Now).Days;

                    Session["UserID"] = _context.Database.SqlQuery<int>("Select Id  From GeneralUsers where UserName = '" + GeneralUser.UserName + "' AND password = '" + GeneralUser.Password + "' ").FirstOrDefault();
                    Session["Ex"] = total_days;
                    Session["UserName"] = obj.UserName.ToString();
                    Session["SuperAdmin"] = obj.UserName.ToString();
                    Session["Company"] = obj.Comid.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var Companies = _context.tbl_Company.ToList();
                    var alert = "1";
                    var ViewModel = new LoginVM
                    {
                        GeneralUser = GeneralUser,
                        alert = alert,
                        Companies = Companies,
                    };
                    return View("Login", ViewModel);
                }
            }
            return View();
        }
        public JsonResult chkPrevUser(string username)
        {
            var prevUser = _context.tbl_GeneralUser.Where(p => p.UserName == username).FirstOrDefault();

            if (prevUser == null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
       

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("aliengineer1808@gmail.com", "Zaeem Khan");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Aliraza1122";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        //UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password),
                        Timeout = 20000
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
        public ActionResult Index()
        {
            int MonthPurchase = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS MonthPurchase FROM PurDetailVehicles where   (DATEPART(MONTH, PurDetailVehicles.Date) = DATEPART(MONTH, GETDATE())) and (DATEPART(YEAR, PurDetailVehicles.Date) = DATEPART(YEAR, GETDATE())) and Comid='" + Session["Company"] + "'").FirstOrDefault();
            int TodayPurchase = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS TodayPurchase FROM PurDetailVehicles where (DATEPART(DAY, PurDetailVehicles.Date) = DATEPART(DAY, GETDATE())) and (DATEPART(MONTH, PurDetailVehicles.Date) = DATEPART(MONTH, GETDATE())) and (DATEPART(YEAR, PurDetailVehicles.Date) = DATEPART(YEAR, GETDATE())) and Comid='" + Session["Company"] + "'").FirstOrDefault();
            int TodaySale = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS TodaySale FROM SWIs where (DATEPART(DAY, SWIs.Date) = DATEPART(DAY, GETDATE())) and (DATEPART(MONTH, SWIs.Date) = DATEPART(MONTH, GETDATE())) and (DATEPART(YEAR, SWIs.Date) = DATEPART(YEAR, GETDATE())) and Comid='" + Session["Company"] + "'").FirstOrDefault();
            int MonthSale = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS MonthSale FROM SWIs where  (DATEPART(MONTH, SWIs.Date) = DATEPART(MONTH, GETDATE())) and (DATEPART(YEAR, SWIs.Date) = DATEPART(YEAR, GETDATE())) and Comid='" + Session["Company"] + "'").FirstOrDefault();
            decimal DailyRecovery = _context.Database.SqlQuery<decimal>("SELECT isnull(sum(TranscationDetails.Cr),0) DailyRecovery FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.HeadId = 1) AND (ThirdLevels.SecondLevelId = 1000002) and TransDate= CONVERT (date, GETDATE()) and ThirdLevels.Comid='" + Session["Company"] + "' and TranscationDetails.Comid='" + Session["Company"] + "'").FirstOrDefault();
            decimal MonthlyRecovery = _context.Database.SqlQuery<decimal>(" SELECT isnull(sum(TranscationDetails.Cr),0) MonthlyRecovery FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.HeadId = 1) AND (ThirdLevels.SecondLevelId = 1000002) and ThirdLevels.Comid='" + Session["Company"] + "' and TranscationDetails.Comid='" + Session["Company"] + "' and (MONTH(TranscationDetails.TransDate) = MONTH(GETDATE()))").FirstOrDefault();
            decimal DailyPayment = _context.Database.SqlQuery<decimal>("SELECT isnull(sum(TranscationDetails.Dr),0) DailyRecovery FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.HeadId = 2) AND (ThirdLevels.SecondLevelId = 2000001) and TransDate= CONVERT (date, GETDATE()) and ThirdLevels.Comid='" + Session["Company"] + "' and TranscationDetails.Comid='" + Session["Company"] + "'").FirstOrDefault();
            decimal MonthlyPayment = _context.Database.SqlQuery<decimal>(" SELECT isnull(sum(TranscationDetails.Dr),0) MonthlyRecovery FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.HeadId = 2) AND (ThirdLevels.SecondLevelId = 2000001) and ThirdLevels.Comid='" + Session["Company"] + "' and TranscationDetails.Comid='" + Session["Company"] + "' and (MONTH(TranscationDetails.TransDate) = MONTH(GETDATE()))").FirstOrDefault();
            decimal DailyDiscount = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(TranscationDetails.Dr), 0) AS DailyDiscount FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.Comid = '" + Session["Company"] + "') AND (ThirdLevels.AccountNo = 5500002) AND (TranscationDetails.TransDate = CONVERT(date, GETDATE())) AND (TranscationDetails.Comid = '" + Session["Company"] + "') ").FirstOrDefault();
            decimal MonthlyDiscount = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(TranscationDetails.Dr), 0) AS MonthlyExpense  FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.Comid = '" + Session["Company"] + "') AND (ThirdLevels.AccountNo = 5500002) AND (MONTH(TranscationDetails.TransDate) = MONTH(GETDATE())) AND (TranscationDetails.Comid = '" + Session["Company"] + "')").FirstOrDefault();
            decimal DailyExpense = _context.Database.SqlQuery<decimal>("SELECT isnull(SUM(TranscationDetails.Dr),0) AS DailyExpense FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.FirstLevelId = 5002) AND (ThirdLevels.Comid = '" + Session["Company"] + "') AND (TranscationDetails.Comid = '" + Session["Company"] + "') and TranscationDetails.TransDate = CONVERT(date, GETDATE()) ").FirstOrDefault();
            decimal MonthlyExpense = _context.Database.SqlQuery<decimal>("SELECT isnull(SUM(TranscationDetails.Dr),0) AS DailyExpense FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.FirstLevelId = 5002) AND (ThirdLevels.Comid = '" + Session["Company"] + "') AND (TranscationDetails.Comid = '" + Session["Company"] + "')  AND (MONTH(TranscationDetails.TransDate) = MONTH(GETDATE())) ").FirstOrDefault();
            var Sales = _context.Database.SqlQuery<decimal>("SELECT (select isnull(sum(TranscationDetails.Cr) ,0)from TranscationDetails where ThirdLevels.AccountNo=TranscationDetails.AccountNo and Comid=TranscationDetails.Comid) as Sales FROM ThirdLevels WHERE (AccountNo = 4400001) and (Comid='" + Session["Company"] + "')").FirstOrDefault();
            var Expenses = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(ex), 0) AS Expenses FROM (SELECT AccountNo, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (ThirdLevels.AccountNo = AccountNo) AND (Comid = Comid)) AS ex FROM ThirdLevels WHERE (HeadId = 5) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1").FirstOrDefault();
            var Receivable = _context.Database.SqlQuery<ReceivableVMQ>("SELECT TOP (10) AccountTitle, openBal + Transbal AS Balance FROM (SELECT AccountTitle, openBal, ddr, ccr, ddr - ccr AS Transbal FROM (SELECT TOP (10) AccountTitle, Cr, Dr, Dr - Cr AS openBal, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ddr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TranscationDetails_1 WHERE (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (SecondLevelId IN (1000002)) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            var Payable = _context.Database.SqlQuery<PayableVMQ>("SELECT TOP (10) AccountTitle, openBal + Transbal AS Balance FROM (SELECT AccountTitle, openBal, ddr, ccr, ccr - ddr AS Transbal FROM (SELECT TOP (10) AccountTitle, Cr, Dr, Cr - Dr AS openBal, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ddr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TranscationDetails_1 WHERE (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (SecondLevelId IN (2000001)) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            var CashBank = _context.Database.SqlQuery<CashBankVMQ>("SELECT TOP (10) AccountTitle, openBal + Transbal AS Balance FROM (SELECT AccountTitle, openBal, ddr, ccr, ddr - ccr AS Transbal FROM (SELECT TOP (10) AccountTitle, Cr, Dr, Dr - Cr AS openBal, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ddr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TranscationDetails_1 WHERE (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (SecondLevelId IN (1000001,1000003)) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            var MonthlyVehicleSale = _context.Database.SqlQuery<MonthlyVehichleSaleVMQ>("SELECT COUNT(*) AS Vehicle,LEFT(DATENAME(MONTH, Date), 3) + '-' + RIGHT('00' + CAST(YEAR(Date) AS VARCHAR), 4) AS Month FROM SWIs where YEAR(Date)=YEAR(GETDATE()) and (Comid = '" + Session["Company"] + "') GROUP BY LEFT(DATENAME(MONTH, Date), 3) + '-' + RIGHT('00' + CAST(YEAR(Date) AS VARCHAR), 4)").ToList();
            var SaleAnalysis = _context.Database.SqlQuery<SaleAnalysisVMQ>("SELECT TotalAmount, TotalReceived, TotalAmount - TotalReceived AS TotalBalance FROM (SELECT ISNULL(SUM(TranscationDetails.Dr), 0) AS TotalAmount, ISNULL(SUM(TranscationDetails.Cr), 0) AS TotalReceived   FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.Comid = '" + Session["Company"] + "') AND (ThirdLevels.SecondLevelId = 1000002) AND (TranscationDetails.Comid = '" + Session["Company"] + "')) AS derivedtbl_1").FirstOrDefault();
            var MonthlyVehichlePurchase = _context.Database.SqlQuery<MonthlyVehichlePurchaseVMQ>("SELECT COUNT(*) AS Vehicle,LEFT(DATENAME(MONTH, Date), 3) + '-' + RIGHT('00' + CAST(YEAR(Date) AS VARCHAR), 4) AS Month FROM PurDetailVehicles where YEAR(Date)=YEAR(GETDATE()) and (Comid = '" + Session["Company"] + "') GROUP BY LEFT(DATENAME(MONTH, Date), 3) + '-' + RIGHT('00' + CAST(YEAR(Date) AS VARCHAR), 4)").ToList();
            var PurchaseAnalysis = _context.Database.SqlQuery<PurchaseAnalysisVMQ>("SELECT TotalAmount, Totalpaid, TotalAmount - Totalpaid AS TotalBalance FROM (SELECT ISNULL(SUM(TranscationDetails.Cr), 0) AS TotalAmount, ISNULL(SUM(TranscationDetails.Dr), 0) AS Totalpaid FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.Comid = '" + Session["Company"] + "') AND (ThirdLevels.SecondLevelId = 2000001) AND (TranscationDetails.Comid = '" + Session["Company"] + "')) AS derivedtbl_1 ").FirstOrDefault();
            var TopPurchases = _context.Database.SqlQuery<PurchaseWVehicleVMQ>("SELECT TOP (10) PurMasterVehicles.Id, PurMasterVehicles.Invid, PurMasterVehicles.Address, PurMasterVehicles.Phone, PurMasterVehicles.Date, PurMasterVehicles.AdditionalCharges, PurMasterVehicles.NetAmount, PurMasterVehicles.GrandTotal, PurMasterVehicles.Total, PurMasterVehicles.Ptotal, PurMasterVehicles.BTotal, PurMasterVehicles.Vtype, PurMasterVehicles.Comid, Suppliers.Name, PurMasterVehicles.AccountNo FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE (PurMasterVehicles.Vtype = 'PINVV') AND (PurMasterVehicles.Comid = '" + Session["Company"] + "') ORDER BY PurMasterVehicles.Invid DESC").ToList();
            var TopSale = _context.Database.SqlQuery<SWIVMQ>("SELECT TOP (10) derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal - (derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, PlanId, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "') ORDER BY derivedtbl_1.Invid DESC").ToList();
            var viewmodel = new DashBoardVM
            {
                //vehicle Purchase
                MonthPurchase = MonthPurchase,
                TodayPurchase = TodayPurchase,
                //vehicle Sale
                TodaySale = TodaySale,
                MonthSale = MonthSale,
                //Recovery
                DailyRecovery = DailyRecovery,
                MonthlyRecovery = MonthlyRecovery,
                //Payment
                DailyPayment = DailyPayment,
                MonthlyPayment = MonthlyPayment,
                //Discunt
                DailyDiscount = DailyDiscount,
                MonthlyDiscount = MonthlyDiscount,
                //Expense
                DailyExpense = DailyExpense,
                MonthlyExpense = MonthlyExpense,
                //Customer Receivable
                Receivable = Receivable,
                //supplier payable
                Payable = Payable,
                //cash and Bank 
                CashBank=CashBank,
                //sale expenses
                Sales = Sales,
                Expenses=Expenses,
                //Graph
                MonthlyVehicleSale = MonthlyVehicleSale,
                SaleAnalysis = SaleAnalysis,
                MonthlyVehichlePurchase = MonthlyVehichlePurchase,
                PurchaseAnalysis = PurchaseAnalysis,
                // Top Purchase
                TopPurchase = TopPurchases,
                // Top Saale
                TopSale = TopSale,
            };
            return View(viewmodel);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}