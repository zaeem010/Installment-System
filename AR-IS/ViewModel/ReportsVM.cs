using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class ReportsVM
    {
        // Deadlist
        public string Month { get; set; }
        public IEnumerable<DeadlistVMQ> Deadlist { get; set; }
        // vehicle Profit
        public IEnumerable<VehicleProfitVMQ> VehicleProfit { get; set; }
        public string Sdate { get; set; }
        public string Edate { get; set; }
        // Purchase Report
        public IEnumerable<Supplier> Supp_list { get; set; }
        public IEnumerable<VehiclePurchaseReportVMQ> VehiclePurchaseReport { get; set; }
        public IEnumerable<PurDetailVehicle> PurDetailVehicleReport { get; set; }
        public IEnumerable<PurchaseReportVMQ> PurchaseReport { get; set; }
        public IEnumerable<PurchaseReportDetailVMQ> PurchaseReportDetail { get; set; }
        //Purchase Summary
        public IEnumerable<VehiclePurchaseSummaryVMQ> VehiclePurchaseSummary { get; set; }
        public IEnumerable<PurchaseSummaryVMQ> PurchaseSummary { get; set; }

        public Supplier Supplier { get; set; }
        //Sale Report
        public IEnumerable<VehicleSaleReportVMQ> VehicleSaleReport { get; set; }
        public IEnumerable<SWI> DetailVehicleReport { get; set; }

        public IEnumerable<SaleReportVMQ> SaleReport { get; set; }
        public IEnumerable<SaleReportDetailVMQ> SaleReportDetail { get; set; }
        //Sale Summary
        public IEnumerable<VehicleSaleSummaryVMQ> VehicleSaleSummary { get; set; }
        public IEnumerable<SaleSummaryVMQ> SaleSummary { get; set; }

        public IEnumerable<Customer> Cus_list { get; set; }
        public Customer Customer { get; set; }
        public Setting Setting { get; set; }

    }
}