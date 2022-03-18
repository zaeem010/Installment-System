using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class PaymentRecoveryController : Controller
    {
        private ApplicationDbContext _context;

        public PaymentRecoveryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PaymentRecovery
        public ActionResult Index()
        {
            //SqlParameter[] parra = new SqlParameter[]
            //{
            //    new SqlParameter("@id", Session["Company"])
            //};
            var paymentRecovery = _context.Database.SqlQuery<PaymentRecoveryVMQ>("SELECT SV.Invid, SV.Comid, (SELECT TOP (1) InsId FROM SaleVehicleInstallments WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS InsId, (SELECT TOP (1) Status FROM SaleVehicleInstallments AS SaleVehicleInstallments_2 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS Status, (SELECT TOP (1) DueDate FROM SaleVehicleInstallments AS SaleVehicleInstallments_3 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS DueDate, (SELECT TOP (1) ReceivedDate FROM SaleVehicleInstallments AS SaleVehicleInstallments_3 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS ReceivedDate, (SELECT TOP (1) InstallmentMonths FROM SaleVehicleInstallments AS SaleVehicleInstallments_4 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS InstallmentMonths, (SELECT TOP (1) AccountNo FROM SaleVehicleInstallments AS SaleVehicleInstallments_5 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS AccountNo, (SELECT TOP (1) PerMonthAmount FROM SaleVehicleInstallments AS SaleVehicleInstallments_6 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS PerMonthAmount, (SELECT TOP (1) Discounts FROM SaleVehicleInstallments AS SaleVehicleInstallments_7 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS Discounts, (SELECT TOP (1) ReceivedAmount FROM SaleVehicleInstallments AS SaleVehicleInstallments_8 WHERE (Status = 'Pending') AND (SV.Invid = Invid) AND (SV.Comid = Comid)) AS ReceivedAmount, Customers.Name FROM SaleVehicleInstallments AS SV INNER JOIN Customers ON SV.AccountNo = Customers.AccountNo WHERE (SV.Comid = '" + Session["Company"] + "') AND (SV.Status = 'Pending') AND (Customers.Comid = '" + Session["Company"] + "') GROUP BY SV.Invid, SV.Comid, Customers.Name").ToList();
            return View(paymentRecovery);
        }
        public ActionResult SaveInstallment(string[] checkeds, string[] Status, int[] InsId, string[] date, decimal[] ReceivedAmount, int [] Invid, TranscationDetail TranscationDetail, int AccountNo, decimal[] Dis)
        {
            for (int i = 0; i < Status.Count(); i++)
            {
               
                string VehicleName  = _context.Database.SqlQuery<string>("SELECT ISNULL(VehicleName, '') + '-' + ISNULL(EngineNo, '') AS VehicleName FROM SWIs WHERE(Comid = '" + Session["Company"] + "') and Invid='" + Invid[i] + "'").FirstOrDefault();
                if (Status[i] != checkeds[i])
                {
                    TranscationDetail.Transid = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.Transid),0)+1 from TranscationDetails WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                    decimal actual = ReceivedAmount[i] - Dis[i];
                    string Description = "Received  Installment No " + InsId[i] + " Against this  " + VehicleName + "    ";
                    _context.Database.ExecuteSqlCommand("UPDATE  SaleVehicleInstallments SET   ReceivedDate ='" + date[i] + "', Status ='Cleared', ReceivedAmount ='" + actual + "' ,  Discounts='" + Dis[i] + "'   where  InsId ='" + InsId[i] + "' AND Comid='" + Session["Company"] + "'  AND (Invid = '" + Invid[i] + "')  ");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','Cash','" + date[i] + "','1100001','" + ReceivedAmount[i] + "',0,'0','SIV','0','" + Session["Company"] + "')");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "',N'" + Description + "','" + date[i] + "','" + AccountNo + "','0','" + ReceivedAmount[i] + "','0','SIV','0','" + Session["Company"] + "')");
                    if (Dis[i] != 0)
                    {
                        _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','Cash','" + date[i] + "','1100001',0,'" + Dis[i] + "','0','SIV','0','" + Session["Company"] + "')");
                        _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails(Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "',N'Discount','" + date[i] + "','5500002','" + Dis[i] + "','0','0','SIV','0','" + Session["Company"] + "')");
                    }
                }
            }
            return RedirectToAction("Index", "PaymentRecovery");

        }
    }
}