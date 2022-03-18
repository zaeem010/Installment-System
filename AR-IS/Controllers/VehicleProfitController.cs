using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class VehicleProfitController : Controller
    {
        private ApplicationDbContext _context;

        public VehicleProfitController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: VehicleProfit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchVehicleProfit(string Sdate ,string Edate)
        {
            var viewModel = new ReportsVM
            {
                Sdate= Sdate,
                Edate=Edate,
                VehicleProfit = _context.Database.SqlQuery<VehicleProfitVMQ>("SELECT Date,VehicleName, EngineNo, CostPrice, SalePrice, SalePrice - CostPrice AS Profit FROM(SELECT Date,VehicleName, EngineNo, NetTotal AS SalePrice, (SELECT ISNULL(SUM(WithGSTTotal), 0) AS Expr1 FROM PurDetailVehicles WHERE (Comid = SWIs.Comid) AND (EngineNo = SWIs.EngineNo) AND (SWIs.Date BETWEEN '" + Sdate+ "' AND '" + Edate + "')) AS CostPrice FROM SWIs WHERE (Date BETWEEN '" + Sdate + "' AND '" + Edate + "') AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1").ToList(),
            };
            return View(viewModel);
        }
        public ActionResult Print(string Sdate, string Edate)
        {
            var viewModel = new ReportsVM
            {
                Sdate = Sdate,
                Edate = Edate,
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                
                VehicleProfit = _context.Database.SqlQuery<VehicleProfitVMQ>("SELECT Date,VehicleName, EngineNo, CostPrice, SalePrice, SalePrice - CostPrice AS Profit FROM(SELECT Date,VehicleName, EngineNo, NetTotal AS SalePrice, (SELECT ISNULL(SUM(WithGSTTotal), 0) AS Expr1 FROM PurDetailVehicles WHERE (Comid = SWIs.Comid) AND (EngineNo = SWIs.EngineNo) AND (SWIs.Date BETWEEN '" + Sdate + "' AND '" + Edate + "')) AS CostPrice FROM SWIs WHERE (Date BETWEEN '" + Sdate + "' AND '" + Edate + "') AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1").ToList(),
            };
            return View(viewModel);
        }


        public ActionResult Ali ()
        {
            Hashtable numberNames = new Hashtable();
            numberNames.Add(1, "One"); //adding a key/value using the Add() method
            numberNames.Add(2, "Two");
            numberNames.Add(3, "Three");

            //The following throws run-time exception: key already added.
            //numberNames.Add(3, "Three"); 

            foreach (DictionaryEntry de in numberNames)
                Console.WriteLine("Key: {0}, Value: {1}", de.Key, de.Value);

            //creating a Hashtable using collection-initializer syntax
            var cities = new Hashtable(){
                {"UK", "London, Manchester, Birmingham"},
                   {"USA", "Chicago, New York, Washington"},
                     {"India", "Mumbai, New Delhi, Pune"}
            };
            foreach (DictionaryEntry de in cities)
                Console.WriteLine("Key: {0}, Value: {1}", de.Key, de.Value);
            return View();
        }
    }
}