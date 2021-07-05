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
                SaleSummary = _context.Database.SqlQuery<SaleSummaryVMQ>("SELECT SWIs.Date, SWIs.VehicleName, SWIs.EngineNo, SWIs.NetTotal, SWIs.Invid, Customers.Name FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE "+varcond+"  AND (SWIs.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SWIs.Invid").ToList(),
                SingleSaleSummary = _context.Database.SqlQuery<SaleSummaryVMQ>("SELECT ISNULL(SUM(NetTotal), 0) AS GrandNetTotal FROM SWIs WHERE   " + varcond + "   AND  (Comid = '" + Session["Company"] + "')").FirstOrDefault(),
            };
            return View(viewModel);
        }
    }
}