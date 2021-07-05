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
            if (Sdate != "" && Edate != "" && Supplier.AccountNo == 0)
            {
                varcond = "(PurMasterVehicles.Date BETWEEN '"+Sdate+"' AND '"+Edate+"')  ";
            }
            else
            {
                varcond = "(PurMasterVehicles.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (PurMasterVehicles.AccountNo = '"+Supplier.AccountNo+"')  ";
            }
            var viewModel = new ReportsVM
            {
                Supplier = Supplier,
                Sdate=Sdate,
                Edate = Edate,
                TotalPurchaseReport = _context.Database.SqlQuery<PurchaseReportVMQ>("SELECT ISNULL(SUM(Total), 0) AS TTotal, ISNULL(SUM(AdditionalCharges), 0) AS TAdditionalCharges, ISNULL(SUM(NetAmount), 0) AS TNetAmount FROM PurMasterVehicles WHERE  "+varcond+ " AND  (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                Supp_list = _context.Database.SqlQuery<Supplier>("SELECT * FROM   Suppliers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                PurchaseReport = _context.Database.SqlQuery<PurchaseReportVMQ>("SELECT PurMasterVehicles.Invid, PurMasterVehicles.Date, PurMasterVehicles.AccountNo, Suppliers.Name, PurMasterVehicles.Total, PurMasterVehicles.AdditionalCharges, PurMasterVehicles.NetAmount FROM PurMasterVehicles INNER JOIN Suppliers ON PurMasterVehicles.AccountNo = Suppliers.AccountNo WHERE  "+varcond+"  AND (PurMasterVehicles.Comid = '" + Session["Company"] + "') AND (Suppliers.Comid = '" + Session["Company"] + "') order by PurMasterVehicles.Invid").ToList(),
            };
            return View(viewModel);
        }

    }
}