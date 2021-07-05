using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class PWIVM
    {
        public IEnumerable<Supplier> Supp_list { get; set; }
        
        public PurDetailVehicle PurDetailVehicle { get; set; }
        public TranscationDetail TranscationDetail { get; set; }
        public Setting Setting { get; set; }
        public IEnumerable<PurchaseVehicleInstallment> PurchaseVehicleInstallment { get; set; }
        public PWIVMQ Vehicleinfo { get; set; }
    }
}