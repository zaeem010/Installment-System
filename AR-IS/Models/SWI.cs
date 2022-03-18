using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class SWI
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Invid { get; set; }
        public int Rid { get; set; }
        public string  Date { get; set; }
        public int Comid { get; set; }
        public string Vtype { get; set; }
        public string KeyNo { get; set; }
        public string Color { get; set; }
        public string Remarks { get; set; }
        public string VehicleName { get; set; }
        public string ModelNo { get; set; }
        public string ChassiNo { get; set; }
        public string EngineNo { get; set; }
        public decimal TotalRate { get; set; }
        public decimal Interests { get; set; }
        public decimal AdvancePayment { get; set; }
        public decimal Discount { get; set; }
        public decimal BalanceTotal { get; set; }
        public decimal NetTotal { get; set; }
        public string Installmentdate { get; set; }
        public decimal FirstInstallment { get; set; }
        public decimal LastInstallment { get; set; }
        public int PlanId { get; set; }
        public string Status { get; set; }
        public int MarkUp { get; set; }
        public String MergingId { get; set; }


    }
}