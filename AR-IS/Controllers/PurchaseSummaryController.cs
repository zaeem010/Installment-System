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
            
            if (Sdate != "" && Edate != "" && Supplier.AccountNo == 0)
            {
                varcond = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
            }
            else
            {
                varcond = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetailVehicles.AccountNo = '" + Supplier.AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Sdate = Sdate,
                Edate = Edate,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                VehiclePurchaseSummary = _context.Database.SqlQuery<VehiclePurchaseSummaryVMQ>("SELECT PurDetailVehicles.Invid, PurDetailVehicles.VehicleName, PurDetailVehicles.Date, PurDetailVehicles.WithGSTTotal, PurDetailVehicles.EngineNo, Suppliers.Name FROM PurDetailVehicles INNER JOIN Suppliers ON PurDetailVehicles.AccountNo = Suppliers.AccountNo WHERE  "+varcond+" AND (PurDetailVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurDetailVehicles.Invid").ToList(),
                
            };
            return View(viewModel);
        }
        public ActionResult Print(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";

            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
            }
            else
            {
                varcond = "(PurDetailVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetailVehicles.AccountNo = '" + AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                
                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                VehiclePurchaseSummary = _context.Database.SqlQuery<VehiclePurchaseSummaryVMQ>("SELECT PurDetailVehicles.Invid, PurDetailVehicles.VehicleName, PurDetailVehicles.Date, PurDetailVehicles.WithGSTTotal, PurDetailVehicles.EngineNo, Suppliers.Name FROM PurDetailVehicles INNER JOIN Suppliers ON PurDetailVehicles.AccountNo = Suppliers.AccountNo WHERE  " + varcond + " AND (PurDetailVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurDetailVehicles.Invid").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult IndexX(Supplier Supplier)
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

            if (Sdate != "" && Edate != "" && Supplier.AccountNo == 0)
            {
                varcond = "(PurDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(PurDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetails.AccountNo = '" + Supplier.AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Sdate = Sdate,
                Edate = Edate,
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                PurchaseSummary = _context.Database.SqlQuery<PurchaseSummaryVMQ>("SELECT        PurDetails.Invid, PurDetails.ItemName, PurDetails.Date, PurDetails.Itemid, PurDetails.Qty, Suppliers.Name,ItemUnit,CTN,NetTotal,Vtype FROM   PurDetails INNER JOIN Suppliers ON PurDetails.AccountNo = Suppliers.AccountNo WHERE  " + varcond + " AND (PurDetails.Vtype='PINVWTC') AND (PurDetails.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurDetails.Invid").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult PrintSummary(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";

            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(PurDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(PurDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurDetails.AccountNo = '" + AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {

                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                PurchaseSummary = _context.Database.SqlQuery<PurchaseSummaryVMQ>("SELECT        PurDetails.Invid, PurDetails.ItemName, PurDetails.Date, PurDetails.Itemid, PurDetails.Qty, Suppliers.Name,ItemUnit,CTN,NetTotal,Vtype FROM   PurDetails INNER JOIN Suppliers ON PurDetails.AccountNo = Suppliers.AccountNo WHERE  " + varcond + " AND (PurDetails.Vtype='PINVWTC') AND (PurDetails.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') ORDER BY PurDetails.Invid").ToList(),

            };
            return View(viewModel);
        }
    }
}