using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class PurDetailVehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Invid { get; set; }
        public string VehicleName { get; set; }
        public string ModelNo { get; set; }
        public string ChassiNo { get; set; }
        public string EngineNo { get; set; }
        public decimal Rate { get; set; }
        public decimal GST { get; set; }
        public string KeyNo { get; set; }
        public decimal WithoutGSTTotal { get; set; }
        public decimal WithGSTTotal { get; set; }
        public decimal Discount { get; set; }
        public string Color { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string NumberOfInstallment { get; set; }
        public string InstallmentDate { get; set; }
        public decimal AdvancePayment { get; set; }
        public int AccountNo { get; set; }
        public decimal BalanceTotal { get; set; }

    }
}