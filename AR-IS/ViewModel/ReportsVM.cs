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
        public FinalProfitVMQ FinalProfit { get; set; }
        public string Sdate { get; set; }
        public string Edate { get; set; }
        // Purchase Report
        public IEnumerable<Supplier> Supp_list { get; set; }
        public IEnumerable<PurchaseReportVMQ> PurchaseReport { get; set; }
        public PurchaseReportVMQ  TotalPurchaseReport { get; set; }
        //Purchase Summary
        public IEnumerable<PurchaseSummaryVMQ> PurchaseSummary { get; set; }
        public PurchaseSummaryVMQ SinglePurchaseSummary { get; set; }
        public Supplier Supplier { get; set; }
        //Sale Report
        public IEnumerable<SaleReportVMQ> SaleReport { get; set; }
        public SaleReportVMQ TotalSaleReport { get; set; }

        //Sale Summary
        public IEnumerable<SaleSummaryVMQ> SaleSummary { get; set; }
        public SaleSummaryVMQ SingleSaleSummary { get; set; }

        public IEnumerable<Customer> Cus_list { get; set; }
        public Customer Customer { get; set; }
        public Setting Setting { get; set; }

    }
}