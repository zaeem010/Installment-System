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
                VehicleSaleReport = _context.Database.SqlQuery<VehicleSaleReportVMQ>("SELECT SWIs.Invid, SWIs.Date, SWIs.VehicleName, SWIs.EngineNo, SWIs.TotalRate, SWIs.Interests, SWIs.Discount, SWIs.NetTotal, Customers.Name FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE  "+varcond+"  AND    (SWIs.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SWIs.Invid").ToList(),
                DetailVehicleReport = _context.Database.SqlQuery<SWI>("SELECT * from SWIs WHERE  " + varcond + "  AND    (SWIs.Comid = '" + Session["Company"] + "')  ORDER BY SWIs.Invid").ToList(),
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
                VehicleSaleReport = _context.Database.SqlQuery<VehicleSaleReportVMQ>("SELECT SWIs.Invid, SWIs.Date, SWIs.VehicleName, SWIs.EngineNo, SWIs.TotalRate, SWIs.Interests, SWIs.Discount, SWIs.NetTotal, Customers.Name FROM SWIs INNER JOIN Customers ON SWIs.AccountNo = Customers.AccountNo WHERE  " + varcond + "  AND    (SWIs.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') ORDER BY SWIs.Invid").ToList(),
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
            string varcond1 = "";
            if (Sdate != "" && Edate != "" && Customer.AccountNo == 0)
            {
                varcond = "(SaleMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
                varcond1 = "(SaleDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";
            }
            else
            {
                varcond = "(SaleMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SaleMasters.AccountNo = '" + Customer.AccountNo + "')  ";
                varcond1 = "(SaleDetails.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SaleDetails.AccountNo = '" + Customer.AccountNo + "')  ";
            }
            var viewModel = new ReportsVM
            {
                Customer = Customer,
                Sdate = Sdate,
                Edate = Edate,
                Cus_list = _context.Database.SqlQuery<Customer>("SELECT * FROM   Customers WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                SaleReport = _context.Database.SqlQuery<SaleReportVMQ>("SELECT SaleMasters.Invid, SaleMasters.Vtype, SaleMasters.Date, SaleMasters.AccountNo, Customers.Name, SaleMasters.Total, SaleMasters.CargoCharges, SaleMasters.DiscountAmount,SaleMasters.GrandTotal ,SaleMasters.NetAmount FROM SaleMasters INNER JOIN Customers ON SaleMasters.AccountNo = Customers.AccountNo WHERE  " + varcond + " AND (SaleMasters.Vtype='SINVWTC') AND (SaleMasters.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') order by SaleMasters.Invid").ToList(),
                SaleReportDetail = _context.Database.SqlQuery<SaleReportDetailVMQ>("SELECT    *  FROM    SaleDetails WHERE " + varcond1 + " AND   (Comid = '" + Session["Company"] + "') AND (Vtype = 'SINVWTC')").ToList(),
            };
            return View(viewModel);
        }

        public ActionResult PrintReport(int AccountNo, string Sdate, string Edate)
        {
            string varcond = "";

            if (Sdate != "" && Edate != "" && AccountNo == 0)
            {
                varcond = "(SaleMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  ";

            }
            else
            {
                varcond = "(SaleMasters.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')  AND (SaleMasters.AccountNo = '" + AccountNo + "')  ";

            }
            var viewModel = new ReportsVM
            {

                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                SaleReport = _context.Database.SqlQuery<SaleReportVMQ>("SELECT SaleMasters.Invid, SaleMasters.Vtype, SaleMasters.Date, SaleMasters.AccountNo, Customers.Name, SaleMasters.Total, SaleMasters.CargoCharges, SaleMasters.DiscountAmount,SaleMasters.GrandTotal ,SaleMasters.NetAmount FROM SaleMasters INNER JOIN Customers ON SaleMasters.AccountNo = Customers.AccountNo WHERE  " + varcond + " AND (SaleMasters.Vtype='SINVWTC') AND (SaleMasters.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') order by SaleMasters.Invid").ToList(),

            };
            return View(viewModel);
        }
    }
}