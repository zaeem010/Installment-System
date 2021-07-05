using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class PurDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Invid { get; set; }
        public int Itemid { get; set; }
        public string ItemName { get; set; }
        public decimal CP { get; set; }
        public decimal Qty { get; set; }
        public decimal NetTotal { get; set; }
        public string Date { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public decimal ItemUnit { get; set; }
        public decimal CTN { get; set; }
    }
}