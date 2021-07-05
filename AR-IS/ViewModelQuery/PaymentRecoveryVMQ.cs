using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    public class PaymentRecoveryVMQ
    {
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string Date { get; set; }
        public int Invid { get; set; }
        public decimal PerMonthAmount { get; set; }
        public decimal Discounts { get; set; }
        public string Status { get; set; }
        public decimal ReceivedAmount { get; set; }
        public int InsId { get; set; }
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public string KeyNo { get; set; }
        public string InstallmentMonths { get; set; }
        public string Name { get; set; }
    }
}