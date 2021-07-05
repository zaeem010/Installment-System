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
            SqlParameter[] parra = new SqlParameter[]
            {
                new SqlParameter("@id", Session["Company"])
            };
            var paymentRecovery = _context.Database.SqlQuery<PaymentRecoveryVMQ>("sp_RecoveryPayment  @id", parra).ToList();
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
                    _context.Database.ExecuteSqlCommand("UPDATE  SaleVehicleInstallments SET   Date ='" + date[i] + "', Status ='Cleared', ReceivedAmount ='" + actual + "' ,  Discounts='" + Dis[i] + "'   where  InsId ='" + InsId[i] + "' AND Comid='" + Session["Company"] + "'  AND (Invid = '" + Invid[i] + "')  ");
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