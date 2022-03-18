using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class SWIVM
    {
        public References References { get; set; }
        public InstallmentPlan InstallmentPlan { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Customer> Cus_list { get; set; }
        public IEnumerable<References> Ref_list { get; set; }
        public IEnumerable<Town> Town_list { get; set; }
        public IEnumerable<Province> Province_list { get; set; }
        public SWI SWI { get; set; }
        public TranscationDetail TranscationDetail { get; set; }
        public IEnumerable<GetVehicleVMQ> Vehiclelist { get; set; }
        public IEnumerable<SWIVMQ> RecentSaleInstallement { get; set; }
        public IEnumerable<SaleVehicleInstallment> SaleVehicleInstallment { get; set; }
        public IEnumerable<InstallmentPlan> InstallmentPlans { get; set; }
        public SWIVMQ Vehicleinfo { get; set; }
        public Setting Setting { get; set; }
        public string wordsinum { get; set; }
        public int status { get; set; }

        public ReceiptPrintVMQ ReceiptPrint { get; set; }
    }
}