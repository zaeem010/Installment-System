using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class SWIController : Controller
    {
        private ApplicationDbContext _context;
        private List<InstallmentPlan> InstallmentPlan;
        public SWIController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: SWI
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<SWIVMQ>("SELECT derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal -(derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, PlanId, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(SWI SWI ,TranscationDetail TranscationDetail,Customer Customer ,References References ,InstallmentPlan Plan)
        {
            var Customers = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList();
            foreach (var item in Customers)
            {
                item.Name = ("" + item.Name + "  " + (item.CNIC)+ "").ToString();
            }
            InstallmentPlan = _context.Database.SqlQuery<InstallmentPlan>("SELECT * FROM   InstallmentPlans  WHERE (Comid = '" + Session["Company"] + "')").ToList();
            foreach (var item in InstallmentPlan)
            {
                item.year = ("" + item.year + "-Year").ToString();
            }
            SWI.Invid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Invid AS int)), 0) + 1 AS Invid FROM   SWIs  WHERE (Comid = '" + Session["Company"] + "') and  Vtype='SINVWI' ").FirstOrDefault();
            var ViewModel = new SWIVM
            {
                SWI = SWI,
                Customer = Customer,
                InstallmentPlans = InstallmentPlan,
                InstallmentPlan = Plan,
                References = References,
                TranscationDetail = TranscationDetail,
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                Vehiclelist =_context.Database.SqlQuery<GetVehicleVMQ>("SELECT ISNULL(VehicleName, '') + '-' + ISNULL(EngineNo, '') AS VehicleName, EngineNo, Invid FROM PurDetailVehicles WHERE(Comid = '" + Session["Company"] + "') AND (EngineNo NOT IN (SELECT SWIs.EngineNo FROM SWIs WHERE (Comid = '" + Session["Company"] + "')))").ToList(),
                Cus_list = Customers,
                Ref_list = _context.Database.SqlQuery<References>("SELECT * FROM  [References] WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                RecentSaleInstallement = _context.Database.SqlQuery<SWIVMQ>("SELECT derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal -(derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, PlanId, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "')").ToList(),
                
            };
            return View(ViewModel);
        }
        public ActionResult Save(SWI SWI, TranscationDetail TranscationDetail,SaleVehicleInstallment SaleVehicleInstallment)
        {
            string varDirection = "";
            decimal Balance;
            string year = _context.Database.SqlQuery<string>("SELECT year FROM   InstallmentPlans  WHERE   (Id = '"+SWI.PlanId + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault();
            decimal Years =Convert.ToDecimal(year) * 12;
            int numberOfInstallment;
            if (SWI.FirstInstallment != 0 && SWI.LastInstallment != 0)
            {
                numberOfInstallment = Convert.ToInt32(Years) - 2;
                Balance = (SWI.BalanceTotal - (SWI.FirstInstallment + SWI.LastInstallment)) / numberOfInstallment;
            }
            else if (SWI.FirstInstallment != 0 && SWI.LastInstallment == 0)
            {
                numberOfInstallment = Convert.ToInt32(Years ) - 1;
                Balance = (SWI.BalanceTotal - (SWI.FirstInstallment)) / numberOfInstallment;
            }
            else if (SWI.FirstInstallment == 0 && SWI.LastInstallment != 0)
            {
                numberOfInstallment = Convert.ToInt32(Years ) - 1;
                Balance = (SWI.BalanceTotal - (SWI.LastInstallment)) / numberOfInstallment;
            }
            else
            {
                numberOfInstallment = Convert.ToInt32(Years);
                Balance = SWI.BalanceTotal / numberOfInstallment;
            }
            DateTime date = Convert.ToDateTime(SWI.Installmentdate).AddMonths(1);
            string Description = "" + SWI.VehicleName + " - " + SWI.EngineNo + " Against this  Invoice No " + SWI.Invid + "  ";
            if (SWI.Id == 0)
            {
                SWI.Vtype = "SINVWI";
                SWI.Comid = Convert.ToInt32(Session["Company"]);
                _context.tbl_SWI.Add(SWI);
                if (SWI.AdvancePayment != SWI.NetTotal)       
                {
                    if (SWI.FirstInstallment != 0)
                    {
                        SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                        _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + SWI.FirstInstallment + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.ToString("yyyy-MM") + "')");

                    }
                    for (int i = 0; i < numberOfInstallment; i++)
                    {
                        if (SWI.FirstInstallment != 0)
                        {
                            SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                            _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(i+1).ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + Balance+ "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.AddMonths(i+1).ToString("yyyy-MM") + "')");
                        }
                        else
                        {
                            SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                            _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(i).ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + Balance + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.AddMonths(i).ToString("yyyy-MM") + "')");
                        }

                    }
                    if (SWI.LastInstallment != 0)
                    {
                        int last = Convert.ToInt32(Years) - 1;
                        SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                        _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(last).ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + SWI.LastInstallment + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.AddMonths(last).ToString("yyyy-MM") + "')");
                    }
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (SWI.AdvancePayment == 0)
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','"+Description+ "','" + SWI.Date + "','" + SWI.AccountNo + "','" + SWI.NetTotal + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Sales','" + SWI.Date + "',4400001,'0','" + SWI.NetTotal + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','CGS','" + SWI.Date + "','5500001','" + SWI.TotalRate + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //5
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + SWI.Date + "',1100002,'0','" + SWI.TotalRate + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + Description + "','" + SWI.Date + "','" + SWI.AccountNo + "','" + SWI.NetTotal + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Sales','" + SWI.Date + "',4400001,'0','" + SWI.NetTotal + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','CGS','" + SWI.Date + "','5500001','" + SWI.TotalRate + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //5
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + SWI.Date + "',1100002,'0','" + SWI.TotalRate + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //6
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','Cash','" + SWI.Date + "','1100001','" + SWI.AdvancePayment + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //7
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Description + "','" + SWI.Date + "','" + SWI.AccountNo + "','0','" + SWI.AdvancePayment + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                }
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM SaleVehicleInstallments  WHERE  (Invid = '" + SWI.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails  WHERE (Vtype = 'SINVWI') AND (Invid = '" + SWI.Invid + "') AND (Comid = '" + Session["Company"] + "') ");
                _context.Database.ExecuteSqlCommand("UPDATE SWIs SET AccountNo ='"+SWI.AccountNo+ "', Invid ='" + SWI.Invid + "', Date ='" + SWI.Date + "', Comid ='" + Session["Company"] + "', Vtype ='SINVWI', VehicleName ='" + SWI.VehicleName + "', ModelNo ='" + SWI.ModelNo + "', KeyNo ='" + SWI.KeyNo + "', Color ='" + SWI.Color + "', Remarks ='" + SWI.Remarks + "', ChassiNo ='" + SWI.ChassiNo + "', EngineNo ='" + SWI.EngineNo + "', PlanId ='" + SWI.PlanId + "', TotalRate ='" + SWI.TotalRate + "', Interests ='" + SWI.Interests + "', AdvancePayment ='" + SWI.AdvancePayment + "', Discount ='" + SWI.Discount + "', BalanceTotal ='" + SWI.BalanceTotal + "', NetTotal =  '" + SWI.NetTotal + "'  where  (Vtype = 'SINVWI') AND (Invid = '" + SWI.Invid + "') AND (Comid = '" + Session["Company"] + "')   ");
                if (SWI.AdvancePayment != SWI.NetTotal)
                {
                    if (SWI.FirstInstallment != 0)
                    {
                        SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                        _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + SWI.FirstInstallment + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.ToString("yyyy-MM") + "')");

                    }
                    for (int i = 0; i < numberOfInstallment; i++)
                    {
                        if (SWI.FirstInstallment != 0)
                        {
                            SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                            _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(i + 1).ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + Balance + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.AddMonths(i + 1).ToString("yyyy-MM") + "')");
                        }
                        else
                        {
                            SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                            _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(i).ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + Balance + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.AddMonths(i).ToString("yyyy-MM") + "')");
                        }

                    }
                    if (SWI.LastInstallment != 0)
                    {
                        int last = Convert.ToInt32(Years) - 1;
                        SaleVehicleInstallment.InsId = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(InsId AS int)), 0) + 1 AS InsId FROM   SaleVehicleInstallments  WHERE (Comid = '" + Session["Company"] + "')  and Invid='" + SWI.Invid + "'").FirstOrDefault();
                        _context.Database.ExecuteSqlCommand("INSERT INTO  SaleVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount,Status, ReceivedAmount ,Discounts,InsId,VehicleName, EngineNo, KeyNo,InstallmentMonths ) VALUES ('" + SWI.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(last).ToString("yyyy-MM-dd") + "','" + SWI.Invid + "','" + SWI.LastInstallment + "','Pending','0','0','" + SaleVehicleInstallment.InsId + "','" + SWI.VehicleName + "','" + SWI.EngineNo + "','" + SWI.KeyNo + "','" + date.AddMonths(last).ToString("yyyy-MM") + "')");
                    }
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (SWI.AdvancePayment == 0)
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + Description + "','" + SWI.Date + "','" + SWI.AccountNo + "','" + SWI.NetTotal + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Sales','" + SWI.Date + "',4400001,'0','" + SWI.NetTotal + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','CGS','" + SWI.Date + "','5500001','" + SWI.TotalRate + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //5
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + SWI.Date + "',1100002,'0','" + SWI.TotalRate + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','" + Description + "','" + SWI.Date + "','" + SWI.AccountNo + "','" + SWI.NetTotal + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Sales','" + SWI.Date + "',4400001,'0','" + SWI.NetTotal + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','CGS','" + SWI.Date + "','5500001','" + SWI.TotalRate + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //5
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + SWI.Date + "',1100002,'0','" + SWI.TotalRate + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //6
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid ,V_No) VALUES " +
               "('" + TranscationDetail.Transid + "','Cash','" + SWI.Date + "','1100001','" + SWI.AdvancePayment + "','0','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                    //7
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','" + Description + "','" + SWI.Date + "','" + SWI.AccountNo + "','0','" + SWI.AdvancePayment + "','" + SWI.Invid + "','SINVWI','" + Session["Company"] + "','0')");
                }
                varDirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "SWI");
        }
       
        public ActionResult SaveInstallment(string [] checkeds, string[] Status, int[] InsId, string[] date, decimal [] ReceivedAmount,int Invid, TranscationDetail TranscationDetail,int AccountNo, decimal [] Dis ,SWI SWI)
        {
            
            string VehicleName= _context.Database.SqlQuery<string>("SELECT ISNULL(VehicleName, '') + '-' + ISNULL(EngineNo, '') AS VehicleName FROM SWIs WHERE(Comid = '" + Session["Company"] + "') and Invid='" + Invid+"'").FirstOrDefault();
            TranscationDetail.Transid = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.Transid),0)+1 from TranscationDetails WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
            for (int i = 0; i < Status.Count(); i++)
            {
                if(Status[i] != checkeds[i])
                {
                     decimal actual  = ReceivedAmount[i] - Dis[i];
                     string Description = "Received  Installment No " + InsId[i] + " Against this  " + VehicleName + "    ";
                    _context.Database.ExecuteSqlCommand("UPDATE  SaleVehicleInstallments SET   Date ='" + date[i] + "', Status ='Cleared', ReceivedAmount ='" + actual + "' ,  Discounts='"+Dis[i]+ "'   where  InsId ='" + InsId[i] + "' AND Comid='" + Session["Company"] + "'  AND (Invid = '" + Invid + "')  ");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','Cash','" + date[i] + "','1100001','" + ReceivedAmount[i] + "',0,'0','SIV','0','" + Session["Company"] + "')");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "',N'"+ Description + "','" + date[i] + "','" + AccountNo + "','0','" + ReceivedAmount[i] + "','0','SIV','0','" + Session["Company"] + "')");
                    if (Dis[i] != 0)
                    {
                        _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','Cash','" + date[i] + "','1100001',0,'" + Dis[i] + "','0','SIV','0','" + Session["Company"] + "')");
                        _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "',N'Discount','" + date[i] + "','5500002','" + Dis[i] + "','0','0','SIV','0','" + Session["Company"] + "')");
                    }
                }
            }
            var ViewModel = new SWIVM
            {
                status = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS Expr1 FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Status = 'Pending')").FirstOrDefault(),
                SaleVehicleInstallment = _context.Database.SqlQuery<SaleVehicleInstallment>("  SELECT * FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')  ").ToList(),
                Vehicleinfo = _context.Database.SqlQuery<SWIVMQ>("SELECT derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal -(derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, NumberOfInstallment, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "') AND (Invid = '" + Invid + "')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
            };
            return View("InstallmentDetail", ViewModel);

        }
        public ActionResult InstallmentDetail(int Invid)
        {
            
            var ViewModel = new SWIVM
            {
                status= _context.Database.SqlQuery<int>("SELECT COUNT(*) AS Expr1 FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Status = 'Pending')").FirstOrDefault(),
                SaleVehicleInstallment = _context.Database.SqlQuery<SaleVehicleInstallment>("SELECT * FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')  ").ToList(),
                Vehicleinfo = _context.Database.SqlQuery<SWIVMQ>("SELECT derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal -(derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, PlanId, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "') AND (Invid = '"+Invid+"')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
            };
            return View("InstallmentDetail", ViewModel);
        }
        public ActionResult Edit(int Invid, TranscationDetail TranscationDetail,Customer Customer)
        {

            int status = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS Expr1 FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Status = 'Cleared')").FirstOrDefault();
            if (status == 0)
            {
                InstallmentPlan = _context.Database.SqlQuery<InstallmentPlan>("SELECT * FROM   InstallmentPlans  WHERE (Comid = '" + Session["Company"] + "')").ToList();
                var Customers = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList();
                foreach (var item in Customers)
                {
                    item.Name = ("" + item.Name + "  " + (item.CNIC) + "").ToString();
                }
                foreach (var item in InstallmentPlan)
                {
                    item.year = ("" + item.year + "-Year").ToString();
                }
                var ViewModel = new SWIVM
                {
                    InstallmentPlans= InstallmentPlan,
                    Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                    Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                    Vehiclelist = _context.Database.SqlQuery<GetVehicleVMQ>("SELECT ISNULL(VehicleName, '') + '-' + ISNULL(EngineNo, '') AS VehicleName, EngineNo, Invid FROM PurDetailVehicles WHERE(Comid = '" + Session["Company"] + "') AND (EngineNo NOT IN (SELECT SWIs.EngineNo FROM SWIs WHERE (Comid = '" + Session["Company"] + "'))) AND (Vtype = 'PINVV')").ToList(),
                    Cus_list = Customers,
                    SWI = _context.Database.SqlQuery<SWI>("SELECT * FROM   SWIs WHERE (Vtype = 'SINVWI') AND (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                    RecentSaleInstallement = _context.Database.SqlQuery<SWIVMQ>("SELECT SWIs.Id, SWIs.AccountNo, SWIs.Invid, SWIs.Date, SWIs.Comid, SWIs.Vtype, SWIs.VehicleName, SWIs.ModelNo, SWIs.KeyNo, SWIs.Color, SWIs.Remarks, SWIs.ChassiNo, SWIs.EngineNo, SWIs.PlanId, SWIs.TotalRate, SWIs.Interests, SWIs.AdvancePayment, SWIs.Discount, SWIs.BalanceTotal, SWIs.NetTotal, Customers.Name FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE(SWIs.Comid = '" + Session["Company"] + "') AND (SWIs.Vtype = 'SINVWI') AND (Customers.Comid = '" + Session["Company"] + "')").ToList(),
                    Customer= Customer,
                };
                return View("New", ViewModel);
            }
            else
            {
                var Saleinvoicelist = _context.Database.SqlQuery<SWIVMQ>("SELECT derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal -(derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, PlanId, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "')").ToList();
                TempData["Reg1"] = "You Can't Edit this Invoice because First Installment has Paid";
                return View("Index", Saleinvoicelist);
            }

        }
        public ActionResult Delete(int Invid)
        {

            int status = _context.Database.SqlQuery<int>("SELECT COUNT(*) AS Expr1 FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Status = 'Cleared')").FirstOrDefault();
            if (status == 0)
            {

                _context.Database.ExecuteSqlCommand("DELETE FROM SaleVehicleInstallments WHERE (Invid = '" + Invid + "')  and  (Comid = '" + Session["Company"] + "')");
                _context.Database.ExecuteSqlCommand("DELETE FROM SWIs WHERE (Vtype='SINVWI')  AND (Invid = '" + Invid + "')  and  (Comid = '" + Session["Company"] + "')");
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails WHERE (Vtype='SINVWI') AND (Invid = '" + Invid + "')  and  (Comid = '" + Session["Company"] + "')");
                TempData["Reg1"] = "Data Delete Successfully";
                return RedirectToAction("Index", "SWI");
            }
            else
            {
                TempData["Reg1"] = "You Can't Edit this Invoice because First Installment had been Paid";
                return RedirectToAction("Index", "SWI");
                
            }

        }
        public ActionResult Print(int Invid)
        {
            decimal NetAmount = _context.Database.SqlQuery<decimal>("SELECT NetTotal  FROM   SWIs  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'SINVWI')").SingleOrDefault();
            var wordsinum = NumberToWords(Decimal.ToInt32(NetAmount));
            var ViewModel = new SWIVM
            {
                wordsinum = wordsinum,
                Vehicleinfo = _context.Database.SqlQuery<SWIVMQ>("SELECT derivedtbl_1.Id, derivedtbl_1.AccountNo, derivedtbl_1.Invid, derivedtbl_1.Date, derivedtbl_1.Comid, derivedtbl_1.Vtype, derivedtbl_1.KeyNo, derivedtbl_1.Color, derivedtbl_1.Remarks, derivedtbl_1.VehicleName, derivedtbl_1.ModelNo, derivedtbl_1.ChassiNo, derivedtbl_1.EngineNo, derivedtbl_1.PlanId, derivedtbl_1.TotalRate, derivedtbl_1.Interests, derivedtbl_1.AdvancePayment, derivedtbl_1.Discount, derivedtbl_1.BalanceTotal, derivedtbl_1.NetTotal, derivedtbl_1.Installmentdate, derivedtbl_1.FirstInstallment, derivedtbl_1.LastInstallment, derivedtbl_1.Received, derivedtbl_1.Discounts, derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment AS ReceivedTotal, derivedtbl_1.NetTotal -(derivedtbl_1.Received + derivedtbl_1.Discounts + derivedtbl_1.AdvancePayment) AS RemainingBalance, Customers.Name, Customers.Phone2, Customers.Phone1, Customers.Email, Customers.CNIC FROM (SELECT Id, AccountNo, Invid, Date, Comid, Vtype, KeyNo, Color, Remarks, VehicleName, ModelNo, ChassiNo, EngineNo, PlanId, TotalRate, Interests, AdvancePayment, Discount, BalanceTotal, NetTotal, Installmentdate, FirstInstallment, LastInstallment, (SELECT ISNULL(SUM(ReceivedAmount), 0) AS Expr1 FROM SaleVehicleInstallments WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Received, (SELECT ISNULL(SUM(Discounts), 0) AS Expr1 FROM SaleVehicleInstallments AS SaleVehicleInstallments_1 WHERE (Invid = SWIs.Invid) AND (Comid = '" + Session["Company"] + "')) AS Discounts FROM SWIs WHERE (Comid = '" + Session["Company"] + "') AND (Invid = '" + Invid+"')) AS derivedtbl_1 INNER JOIN Customers ON derivedtbl_1.AccountNo = Customers.AccountNo WHERE (derivedtbl_1.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                SaleVehicleInstallment = _context.Database.SqlQuery<SaleVehicleInstallment>("  SELECT * FROM SaleVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')  ").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
            };
            return View("Print", ViewModel);
        }
        public ActionResult Action(string id)
        {
            var getfees = _context.Database.SqlQuery<PurDetailVehicle>("SELECT *  FROM   PurDetailVehicles WHERE (Comid = '" + Session["Company"] + "')  AND (EngineNo = '"+id+"')").ToList();
            return Json(getfees, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Action1(int id)
        {
            var getfees = _context.Database.SqlQuery<InstallmentPlan>("SELECT *  FROM   InstallmentPlans  WHERE (Comid = '" + Session["Company"] + "')  AND (Id = '" + id + "')").ToList();
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
        public ActionResult Save_cus(Customer Customer, ThirdLevel Thirdlevel, HttpPostedFileBase img)
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
                Customer.AccountNo = account_no1;
                Customer.Image = ImageName;
                _context.tbl_Customer.Add(Customer);
                Customer.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("insert into Thirdlevels (AccountNo,FirstLevelId,HeadId,AccountTitle,AccountType,cr,dr,SecondLevelId,Comid) values(" + account_no1 + ",1001,1,N'" + Customer.Name + "','Customer',0,0,'1000002','" + Session["Company"] + "')");
                vardirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            
            return RedirectToAction(vardirection, "SWI");
        }
        public ActionResult Save_Ref(References References, ThirdLevel Thirdlevel, HttpPostedFileBase img)
        {
            string vardirection = "";
            string ImageName = "";
            
            string physicalpath;
            if (img != null)
            {
                ImageName = System.IO.Path.GetFileName(img.FileName);
                physicalpath = Server.MapPath("~/uploads/" + ImageName);
                img.SaveAs(physicalpath);
            }
            if (References.id == 0)
            {
                References.Image = ImageName;
                _context.tbl_References.Add(References);
                References.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();

                vardirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            
            return RedirectToAction(vardirection, "SWI");
        }
        public ActionResult Save_Plan(InstallmentPlan InstallmentPlan)
        {
            string varDirection = "";
            if (InstallmentPlan.Id == 0)
            {
                _context.tbl_InstallmentPlan.Add(InstallmentPlan);
                InstallmentPlan.Comid = Convert.ToInt32(Session["Company"]);
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "SWI");
        }
    }
}