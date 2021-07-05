using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class PWIController : Controller
    {
        private ApplicationDbContext _context;

        public PWIController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PWI
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<PWIVMQ>("SELECT PurDetailVehicles.Id, PurDetailVehicles.Invid, PurDetailVehicles.VehicleName, PurDetailVehicles.ModelNo, PurDetailVehicles.ChassiNo, PurDetailVehicles.EngineNo, PurDetailVehicles.Rate, PurDetailVehicles.GST, PurDetailVehicles.KeyNo, PurDetailVehicles.WithoutGSTTotal, PurDetailVehicles.WithGSTTotal, PurDetailVehicles.Discount, PurDetailVehicles.Color, PurDetailVehicles.Remarks, PurDetailVehicles.Date, PurDetailVehicles.Vtype, PurDetailVehicles.Comid, PurDetailVehicles.NumberOfInstallment, PurDetailVehicles.InstallmentDate, PurDetailVehicles.AdvancePayment, PurDetailVehicles.BalanceTotal, PurDetailVehicles.AccountNo, Suppliers.Name FROM PurDetailVehicles INNER JOIN Suppliers ON PurDetailVehicles.AccountNo = Suppliers.AccountNo WHERE(PurDetailVehicles.Comid = '1') AND (PurDetailVehicles.Vtype = 'PINVWI')").ToList());
        }
        public ActionResult New( TranscationDetail TranscationDetail,PurDetailVehicle PurDetailVehicle )
        {
            PurDetailVehicle.Invid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Invid AS int)), 0) + 1 AS Invid FROM   PurDetailVehicles  WHERE (Comid = '" + Session["Company"] + "') and  Vtype='PINVWI' ").FirstOrDefault();
            var ViewModel = new PWIVM
            {
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                TranscationDetail = TranscationDetail,
                PurDetailVehicle= PurDetailVehicle,
            };
            return View(ViewModel);
        }

        public ActionResult Save(TranscationDetail TranscationDetail, PurDetailVehicle PurDetailVehicle, decimal GST)
        {
            string varDirection = "";
            if (PurDetailVehicle.Id == 0)
            {
                _context.tbl_PurDetailVehicle.Add(PurDetailVehicle);
                PurDetailVehicle.Comid = Convert.ToInt32(Session["Company"]);
                PurDetailVehicle.Vtype = "PINVWI";
                PurDetailVehicle.GST = GST;
                int numberOfInstallment = Convert.ToInt32(PurDetailVehicle.NumberOfInstallment);
                decimal Balance = PurDetailVehicle.BalanceTotal / numberOfInstallment;
                DateTime date = Convert.ToDateTime(PurDetailVehicle.InstallmentDate).AddMonths(1);
                string insid = "Code-" + PurDetailVehicle.Invid + "-" + PurDetailVehicle.EngineNo + "";
                if (PurDetailVehicle.AdvancePayment != PurDetailVehicle.WithGSTTotal)
                {
                    for (int i = 0; i < numberOfInstallment; i++)
                    {
                        _context.Database.ExecuteSqlCommand("INSERT INTO  PurchaseVehicleInstallments(AccountNo, Comid, Date, Invid, PerMonthAmount, InsId,Status, PaidAmount ) VALUES ('" + PurDetailVehicle.AccountNo + "','" + Session["Company"] + "','" + date.AddMonths(i).ToString("yyyy-MM-dd") + "','" + PurDetailVehicle.Invid + "','" + Balance + "','" + insid + "','Pending','0')");
                    }
                }
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (PurDetailVehicle.AdvancePayment == 0)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurDetailVehicle.Date + "','" + PurDetailVehicle.AccountNo + "','0','" + PurDetailVehicle.WithGSTTotal + "','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurDetailVehicle.Date + "',1100002,'" + PurDetailVehicle.WithGSTTotal + "','0','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurDetailVehicle.Date + "','" + PurDetailVehicle.AccountNo + "','0','" + PurDetailVehicle.WithGSTTotal + "','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurDetailVehicle.Date + "',1100002,'" + PurDetailVehicle.WithGSTTotal + "','0','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Supplier','" + PurDetailVehicle.Date + "','" + PurDetailVehicle.AccountNo + "','" + PurDetailVehicle.AdvancePayment + "','0','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                        "('" + TranscationDetail.Transid + "','Cash','" + PurDetailVehicle.Date + "',1100001,'0','" + PurDetailVehicle.AdvancePayment + "','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                }
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {

                TranscationDetail.Transid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Transid AS int)), 0) + 1 AS Transid  FROM   TranscationDetails  WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                if (PurDetailVehicle.AdvancePayment == 0)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks ,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurDetailVehicle.Date + "','" + PurDetailVehicle.AccountNo + "','0','" + PurDetailVehicle.WithGSTTotal + "','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");

                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurDetailVehicle.Date + "',1100002,'" + PurDetailVehicle.WithGSTTotal + "','0','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                }
                else
                {
                    //1
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid, Remarks,V_No) VALUES " +
                "('" + TranscationDetail.Transid + "','Supplier','" + PurDetailVehicle.Date + "','" + PurDetailVehicle.AccountNo + "','0','" + PurDetailVehicle.WithGSTTotal + "','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','" + TranscationDetail.Remarks + "','0')");
                    //2
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Stock','" + PurDetailVehicle.Date + "',1100002,'" + PurDetailVehicle.WithGSTTotal + "','0','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                    //3
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                    "('" + TranscationDetail.Transid + "','Supplier','" + PurDetailVehicle.Date + "','" + PurDetailVehicle.AccountNo + "','" + PurDetailVehicle.AdvancePayment + "','0','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                    //4
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype, Comid,V_No) VALUES " +
                        "('" + TranscationDetail.Transid + "','Cash','" + PurDetailVehicle.Date + "',1100001,'0','" + PurDetailVehicle.AdvancePayment + "','" + PurDetailVehicle.Invid + "','PINVWI','" + Session["Company"] + "','0')");
                }
                varDirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "PWI");
        }
        public ActionResult Edit(int Invid, TranscationDetail TranscationDetail)
        {
            var ViewModel = new PWIVM
            {
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
          
            };
            return View("New", ViewModel);
        }
        public ActionResult InstallmentDetail(int Invid)
        {
            var ViewModel = new PWIVM
            {
                PurchaseVehicleInstallment = _context.Database.SqlQuery<PurchaseVehicleInstallment>("  SELECT * FROM PurchaseVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')  ").ToList(),
                Vehicleinfo = _context.Database.SqlQuery<PWIVMQ>(" SELECT PurDetailVehicles.Id, PurDetailVehicles.Invid, PurDetailVehicles.VehicleName, PurDetailVehicles.ModelNo, PurDetailVehicles.ChassiNo, PurDetailVehicles.EngineNo, PurDetailVehicles.Rate, PurDetailVehicles.GST, PurDetailVehicles.KeyNo, PurDetailVehicles.WithoutGSTTotal, PurDetailVehicles.WithGSTTotal, PurDetailVehicles.Discount, PurDetailVehicles.Color, PurDetailVehicles.Remarks, PurDetailVehicles.Date, PurDetailVehicles.Vtype, PurDetailVehicles.Comid, PurDetailVehicles.NumberOfInstallment, PurDetailVehicles.InstallmentDate, PurDetailVehicles.AdvancePayment, PurDetailVehicles.BalanceTotal, PurDetailVehicles.AccountNo, Suppliers.Name FROM PurDetailVehicles INNER JOIN Suppliers ON PurDetailVehicles.AccountNo = Suppliers.AccountNo WHERE(PurDetailVehicles.Vtype = 'PINVWI') AND (PurDetailVehicles.Comid = '"+ Session["Company"] + "') AND (PurDetailVehicles.Invid = '" + Invid+"')").FirstOrDefault(),
            };
            return View("InstallmentDetail", ViewModel);
        }
        public ActionResult SaveInstallment(string[] checkeds, string[] Status, int[] id, string[] date, decimal[] ReceivedAmount, int Invid, TranscationDetail TranscationDetail, int[] AccountNo)
        {
            string VehicleName = _context.Database.SqlQuery<string>("SELECT ISNULL(VehicleName, '') + '-' + ISNULL(EngineNo, '') AS VehicleName  FROM   PurDetailVehicles  WHERE (Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'PINVWI')").FirstOrDefault();
            TranscationDetail.Transid = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.Transid),0)+1 from TranscationDetails WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
            for (int i = 0; i < Status.Count(); i++)
            {
                if (Status[i] != checkeds[i])
                {
                    _context.Database.ExecuteSqlCommand("UPDATE  SaleVehicleInstallments SET   Date ='" + date[i] + "', Status ='Clear', ReceivedAmount ='" + ReceivedAmount[i] + "'   where  id ='" + id[i] + "' AND Comid='" + Session["Company"] + "'");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','Cash','" + date[i] + "','1100001',0,'" + ReceivedAmount[i] + "','-1','PIV','0','" + Session["Company"] + "')");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid,Remarks) VALUES ('" + TranscationDetail.Transid + "',N'Supplier','" + date[i] + "','" + AccountNo[i] + "','" + ReceivedAmount[i] + "',0,'-1','PIV','0','" + Session["Company"] + "','" + VehicleName + "')");

                }
            }
            var ViewModel = new PWIVM
            {
                PurchaseVehicleInstallment = _context.Database.SqlQuery<PurchaseVehicleInstallment>("  SELECT * FROM PurchaseVehicleInstallments WHERE(Invid = '" + Invid + "') AND (Comid = '" + Session["Company"] + "')  ").ToList(),
                Vehicleinfo = _context.Database.SqlQuery<PWIVMQ>(" SELECT PurDetailVehicles.Id, PurDetailVehicles.Invid, PurDetailVehicles.VehicleName, PurDetailVehicles.ModelNo, PurDetailVehicles.ChassiNo, PurDetailVehicles.EngineNo, PurDetailVehicles.Rate, PurDetailVehicles.GST, PurDetailVehicles.KeyNo, PurDetailVehicles.WithoutGSTTotal, PurDetailVehicles.WithGSTTotal, PurDetailVehicles.Discount, PurDetailVehicles.Color, PurDetailVehicles.Remarks, PurDetailVehicles.Date, PurDetailVehicles.Vtype, PurDetailVehicles.Comid, PurDetailVehicles.NumberOfInstallment, PurDetailVehicles.InstallmentDate, PurDetailVehicles.AdvancePayment, PurDetailVehicles.BalanceTotal, PurDetailVehicles.AccountNo, Suppliers.Name FROM PurDetailVehicles INNER JOIN Suppliers ON PurDetailVehicles.AccountNo = Suppliers.AccountNo WHERE(PurDetailVehicles.Vtype = 'PINVWI') AND (PurDetailVehicles.Comid = '" + Session["Company"] + "') AND (PurDetailVehicles.Invid = '" + Invid + "')").FirstOrDefault(),
            };
            return View("InstallmentDetail", ViewModel);

        }
    }
}