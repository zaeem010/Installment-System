using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class PurchaseVehicleInstallment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string Date { get; set; }
        public int Invid { get; set; }
        public decimal PerMonthAmount { get; set; }
        public string InsId { get; set; }
        public string Status { get; set; }
        public decimal PaidAmount { get; set; }
    }
}