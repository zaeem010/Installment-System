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
    public class DeadListController : Controller
    {
        private ApplicationDbContext _context;

        public DeadListController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: DeadList
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchDeadlist(string month)
        {
            var viewModel = new ReportsVM
            {
                Month=month,
                Deadlist = _context.Database.SqlQuery<DeadlistVMQ>("SELECT SaleVehicleInstallments.Id, SaleVehicleInstallments.AccountNo, SaleVehicleInstallments.Invid, SaleVehicleInstallments.PerMonthAmount, SaleVehicleInstallments.Status, SaleVehicleInstallments.ReceivedAmount, SaleVehicleInstallments.Discounts, SaleVehicleInstallments.InsId, SaleVehicleInstallments.VehicleName, SaleVehicleInstallments.EngineNo, SaleVehicleInstallments.KeyNo, Customers.Phone1, Customers.Name, Customers.CNIC, Customers.Email, SaleVehicleInstallments.InstallmentMonths FROM SaleVehicleInstallments INNER JOIN Customers ON SaleVehicleInstallments.AccountNo = Customers.AccountNo WHERE(SaleVehicleInstallments.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') AND (SaleVehicleInstallments.Status = 'Pending') AND (SaleVehicleInstallments.InstallmentMonths = '"+month+"') ").ToList(),
                //Deadlist = _context.Database.SqlQuery<DeadlistVMQ>("SELECT SaleVehicleInstallments.Id, SaleVehicleInstallments.AccountNo, SaleVehicleInstallments.Comid, SaleVehicleInstallments.Date, SaleVehicleInstallments.Invid, SaleVehicleInstallments.PerMonthAmount, SaleVehicleInstallments.Status, SaleVehicleInstallments.ReceivedAmount, SaleVehicleInstallments.Discounts, SaleVehicleInstallments.InsId, SaleVehicleInstallments.VehicleName, SaleVehicleInstallments.EngineNo, SaleVehicleInstallments.KeyNo, LEFT(DATENAME(MONTH, SaleVehicleInstallments.Date), 3) + '-' + RIGHT('00' + CAST(YEAR(SaleVehicleInstallments.Date) AS VARCHAR), 4) AS Month, Customers.Phone, Customers.Name, Customers.CNIC, Customers.Email FROM SaleVehicleInstallments INNER JOIN Customers ON SaleVehicleInstallments.AccountNo = Customers.AccountNo WHERE(CONVERT(varchar(7), SaleVehicleInstallments.Date, 126) = '" + month+ "') AND (SaleVehicleInstallments.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') AND (SaleVehicleInstallments.Status = 'Pending') ").ToList(),
            };
            return View(viewModel);
        }

        public ActionResult Print(string month)
        {
            var viewModel = new ReportsVM
            {
                Month = month,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                Deadlist = _context.Database.SqlQuery<DeadlistVMQ>("SELECT SaleVehicleInstallments.Id, SaleVehicleInstallments.AccountNo, SaleVehicleInstallments.Invid, SaleVehicleInstallments.PerMonthAmount, SaleVehicleInstallments.Status, SaleVehicleInstallments.ReceivedAmount, SaleVehicleInstallments.Discounts, SaleVehicleInstallments.InsId, SaleVehicleInstallments.VehicleName, SaleVehicleInstallments.EngineNo, SaleVehicleInstallments.KeyNo, Customers.Phone1, Customers.Name, Customers.CNIC, Customers.Email, SaleVehicleInstallments.InstallmentMonths FROM SaleVehicleInstallments INNER JOIN Customers ON SaleVehicleInstallments.AccountNo = Customers.AccountNo WHERE(SaleVehicleInstallments.Comid = '" + Session["Company"] + "') AND (Customers.Comid = '" + Session["Company"] + "') AND (SaleVehicleInstallments.Status = 'Pending') AND (SaleVehicleInstallments.InstallmentMonths = '" + month + "') ").ToList(),
                
            };
            return View(viewModel);
        }
    }
}