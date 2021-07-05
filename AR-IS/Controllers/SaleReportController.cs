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
    public class SaleReportController : Controller
    {
        private ApplicationDbContext _context;

        public SaleReportController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: SaleReport
        public ActionResult Index(Customer Customer)
        {
            var viewModel = new ReportsVM
            {
                Customer = Customer,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult SearchSale(Customer Customer, string Sdate, string Edate)
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
                Sdate = Sdate,
                Edate = Edate,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                SaleReport = _context.Database.SqlQuery<SaleReportVMQ>("SELECT SWIs.Invid, SWIs.Date, SWIs.VehicleName, SWIs.EngineNo, SWIs.TotalRate, SWIs.Interests, SWIs.Discount, SWIs.NetTotal, Customers.Name FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE  "+varcond+"  AND    (SWIs.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SWIs.Invid").ToList(),
                TotalSaleReport = _context.Database.SqlQuery<SaleReportVMQ>("SELECT ISNULL(SUM(TotalRate), 0) AS TRate, ISNULL(SUM(Interests), 0) AS TInterests, ISNULL(SUM(Discount), 0) AS TDiscount, ISNULL(SUM(NetTotal), 0) AS GrandNetTotal FROM SWIs WHERE   " + varcond + "  AND  (Comid =  '" + Session["Company"] + "' )").FirstOrDefault(),
            };
            return View(viewModel);
        }
    }
}