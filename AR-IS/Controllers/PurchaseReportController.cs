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
    public class PurchaseReportController : Controller
    {
       
        private ApplicationDbContext _context;

        public PurchaseReportController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PurchaseReport
        public ActionResult Index(Supplier Supplier)
        {
            var viewModel = new ReportsVM
            {
                Supplier= Supplier,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult SearchPurchase(Supplier Supplier, string Sdate, string Edate)
        {
            string varcond = "";
            string varcond1 = "";
            if (Sdate != "" && Edate != "" && Supplier.AccountNo == 0)
            {
                varcond = "(PurMasterVehicles.Date BETWEEN '"+Sdate+"' AND '"+Edate+"')  ";
                varcond1 = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
            }
            else
            {
                varcond = "(PurMasterVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurMasterVehicles.AccountNo = '"+Supplier.AccountNo+"')  ";
                varcond1 = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetailVehicles.AccountNo = '" + Supplier.AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Sdate=Sdate,
                Edate = Edate,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                VehiclePurchaseReport = _context.Database.SqlQuery<VehiclePurchaseReportVMQ>("SELECT PurMasterVehicles.Invid, PurMasterVehicles.Date, PurMasterVehicles.AccountNo, Suppliers.Name, PurMasterVehicles.Total, PurMasterVehicles.CargoCharges, PurMasterVehicles.NetAmount FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE  " + varcond+"  AND (PurMasterVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') order by PurMasterVehicles.Invid").ToList(),
                PurDetailVehicleReport=_context.Database.SqlQuery<PurDetailVehicle>("SELECT     *   FROM     PurDetailVehicles WHERE " + varcond1 + "  AND (PurDetailVehicles.Comid = '" + Session["Company"] + "')  order by PurDetailVehicles.Invid").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult Print(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";
            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(PurMasterVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
            }
            else
            {
                varcond = "(PurMasterVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurMasterVehicles.AccountNo = '" + AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                VehiclePurchaseReport = _context.Database.SqlQuery<VehiclePurchaseReportVMQ>("SELECT PurMasterVehicles.Invid, PurMasterVehicles.Date, PurMasterVehicles.AccountNo, Suppliers.Name, PurMasterVehicles.Total, PurMasterVehicles.CargoCharges, PurMasterVehicles.NetAmount FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE  " + varcond + "  AND (PurMasterVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') order by PurMasterVehicles.Invid").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult Indexx(Supplier Supplier)
        {
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult Search(Supplier Supplier, string Sdate, string Edate)
        {
            string varcond = "";
            string varcond1 = "";
            if (Sdate != "" && Edate != "" && Supplier.AccountNo == 0)
            {
                varcond = "(PurMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
                varcond1 = "(PurDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
            }
            else
            {
                varcond = "(PurMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurMasters.AccountNo = '" + Supplier.AccountNo + "')  ";
                varcond1 = "(PurDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetails.AccountNo = '" + Supplier.AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Sdate = Sdate,
                Edate = Edate,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                PurchaseReport = _context.Database.SqlQuery<PurchaseReportVMQ>("SELECT PurMasters.Invid, PurMasters.Vtype, PurMasters.Date, PurMasters.AccountNo, Suppliers.Name, PurMasters.Total, PurMasters.CargoCharges, PurMasters.DiscountAmount,PurMasters.GrandTotal ,PurMasters.NetAmount FROM PurMasters INNER JOIN Suppliers ON PurMasters.AccountNo = Suppliers.AccountNo WHERE  " + varcond + " AND (PurMasters.Vtype='PINVWTC') AND (PurMasters.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') order by PurMasters.Invid").ToList(),
                PurchaseReportDetail = _context.Database.SqlQuery<PurchaseReportDetailVMQ>("SELECT    *  FROM    PurDetails WHERE " + varcond1 + " AND   (Comid = '" + Session["Company"] + "') AND (Vtype = 'PINVWTC')").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult PrintReport(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";

            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(PurMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(PurMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurMasters.AccountNo = '" + AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {

                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                PurchaseReport = _context.Database.SqlQuery<PurchaseReportVMQ>("SELECT PurMasters.Invid, PurMasters.Vtype, PurMasters.Date, PurMasters.AccountNo, Suppliers.Name, PurMasters.Total, PurMasters.CargoCharges, PurMasters.DiscountAmount,PurMasters.GrandTotal ,PurMasters.NetAmount FROM PurMasters INNER JOIN Suppliers ON PurMasters.AccountNo = Suppliers.AccountNo WHERE  " + varcond + " AND (PurMasters.Vtype='PINVWTC') AND (PurMasters.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') order by PurMasters.Invid").ToList(),

            };
            return View(viewModel);
        }

    }
}