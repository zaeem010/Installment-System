using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class PurchaseVehicleVM
    {
        public Supplier Supplier { get; set; }
        public IEnumerable<Supplier> Supp_list { get; set; }
        public IEnumerable<PurDetailVehicle> PurDetailVehicle_list { get; set; }
        public PurMasterVehicle PurMasterVehicle { get; set; }
        public TranscationDetail TranscationDetail { get; set; }
        public PrintPurchaseVehicleVMQ PrintPurchase { get; set; }
        public Setting Setting { get; set; }
        public string wordsinum { get; set; }
        public IEnumerable<PurchaseWVehicleVMQ> PurchaseRecent { get; set; }
        public IEnumerable<Town> Town_list { get; set; }
        public IEnumerable<Province> Province_list { get; set; }
    }
}