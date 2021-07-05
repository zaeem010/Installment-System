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
    public class PurchaseSummaryController : Controller
    {
        private ApplicationDbContext _context;

        public PurchaseSummaryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PurchaseSummary
        public ActionResult Index(Supplier Supplier)
        {
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult SearchSummary(Supplier Supplier, string Sdate, string Edate)
        {
            string varcond = "";
            string varcond1 = "";
            if (Sdate != "" && Edate != "" && Supplier.AccountNo == 0)
            {
                varcond = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
                varcond1 = "(PurMasterVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";


            }
            else
            {
                varcond = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetailVehicles.AccountNo = '" + Supplier.AccountNo + "')  ";
                varcond1 = "(PurMasterVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurMasterVehicles.AccountNo = '" + Supplier.AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Sdate = Sdate,
                Edate = Edate,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                PurchaseSummary = _context.Database.SqlQuery<PurchaseSummaryVMQ>("SELECT PurDetailVehicles.Invid, PurDetailVehicles.VehicleName, PurDetailVehicles.Date, PurDetailVehicles.WithGSTTotal, PurDetailVehicles.EngineNo, Suppliers.Name FROM PurDetailVehicles INNER JOIN Suppliers ON PurDetailVehicles.AccountNo = Suppliers.AccountNo WHERE  "+varcond+" AND (PurDetailVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurDetailVehicles.Invid").ToList(),
                SinglePurchaseSummary = _context.Database.SqlQuery<PurchaseSummaryVMQ>("SELECT ISNULL(SUM(Total), 0) AS TTotal, ISNULL(SUM(AdditionalCharges), 0) AS TAdditionalCharges, ISNULL(SUM(NetAmount), 0) AS TNetAmount FROM PurMasterVehicles WHERE  " + varcond1 + " AND  (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
            };
            return View(viewModel);
        }
    }
}