using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Bid { get; set; }
        public int Cid { get; set; }
        public string Iname { get; set; }
        public decimal Cprice { get; set; }
        public decimal Sprice { get; set; }
        public decimal Openingstock { get; set; }
        public string Shelfnumber { get; set; }
        public int MeasuringUnit { get; set; }
        public string Barcode { get; set; }
        public string Reorderlevel { get; set; }
        public string Image { get; set; }
        public decimal Itemunit { get; set; }
        public int Comid { get; set; }
    }
}