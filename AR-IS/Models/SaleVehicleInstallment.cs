using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class SaleVehicleInstallment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string ReceivedDate { get; set; }
        public string DueDate { get; set; }
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
    }
}