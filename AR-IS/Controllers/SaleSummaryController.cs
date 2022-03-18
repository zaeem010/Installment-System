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
    public class SaleSummaryController : Controller
    {
        private ApplicationDbContext _context;

        public SaleSummaryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: SaleSummary
        public ActionResult Index(Customer Customer)
        {
            var viewModel = new ReportsVM
            {
                Customer = Customer,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult SearchSummary(Customer Customer , string Sdate, string Edate)
        {
            string varcond = "";
            if (Sdate != "" && Edate != "" && Customer.AccountNo == 0)
            {
                varcond = "(SWIs.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(SWIs.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SWIs.AccountNo = '" + Customer.AccountNo + "')  ";
                
            }
            var viewModel = new ReportsVM
            {
                Customer = Customer,
                Sdate=Sdate,
                Edate = Edate,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                VehicleSaleSummary = _context.Database.SqlQuery<VehicleSaleSummaryVMQ>("SELECT SWIs.Date,SWIs.Color , SWIs.VehicleName, SWIs.EngineNo, SWIs.NetTotal, SWIs.Invid, Customers.Name, SWIs.KeyNo,SWIs.ChassiNo , SWIs.Remarks, SWIs.ModelNo FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE " + varcond + "  AND (SWIs.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SWIs.Invid").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult Print(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";
            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(SWIs.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(SWIs.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SWIs.AccountNo = '" + AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {
                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                VehicleSaleSummary = _context.Database.SqlQuery<VehicleSaleSummaryVMQ>("SELECT SWIs.Date,SWIs.Color , SWIs.VehicleName, SWIs.EngineNo, SWIs.NetTotal, SWIs.Invid, Customers.Name, SWIs.KeyNo,SWIs.ChassiNo , SWIs.Remarks, SWIs.ModelNo FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE " + varcond + "  AND (SWIs.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SWIs.Invid").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult IndexX(Customer Customer)
        {
            var viewModel = new ReportsVM
            {
                Customer = Customer,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult Search(Customer Customer, string Sdate, string Edate)
        {
            string varcond = "";
            if (Sdate != "" && Edate != "" && Customer.AccountNo == 0)
            {
                varcond = "(SaleDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(SaleDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SaleDetails.AccountNo = '" + Customer.AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {
                Customer = Customer,
                Sdate = Sdate,
                Edate = Edate,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                SaleSummary = _context.Database.SqlQuery<SaleSummaryVMQ>("SELECT     SaleDetails.Invid, SaleDetails.ItemName, SaleDetails.Date, SaleDetails.Itemid, SaleDetails.Qty, Customers.Name,ItemUnit,CTN,NetTotal,Vtype FROM   SaleDetails INNER JOIN Customers ON SaleDetails.AccountNo = Customers.AccountNo WHERE  " + varcond + " AND (SaleDetails.Vtype='SINVWTC') AND (SaleDetails.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SaleDetails.Invid").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult PrintSummary(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";
            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(SaleDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(SaleDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SaleDetails.AccountNo = '" + AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {
                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                SaleSummary = _context.Database.SqlQuery<SaleSummaryVMQ>("SELECT  SaleDetails.Invid, SaleDetails.ItemName, SaleDetails.Date, SaleDetails.Itemid, SaleDetails.Qty, Customers.Name,ItemUnit,CTN,NetTotal,Vtype FROM   SaleDetails INNER JOIN Customers ON SaleDetails.AccountNo = Customers.AccountNo WHERE  " + varcond + " AND (SaleDetails.Vtype='SINVWTC') AND (SaleDetails.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SaleDetails.Invid").ToList(),

            };
            return View(viewModel);
        }
    }
}