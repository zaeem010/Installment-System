using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    public class ProductVMQ
    {
        
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Iname { get; set; }
        public decimal Cprice { get; set; }
        public decimal Sprice { get; set; }
        public decimal Openingstock { get; set; }
        public decimal Itemunit { get; set; }
        public string StockDate { get; set; }
        public string Barcode { get; set; }
        public string Reorderlevel { get; set; }
        public string Image { get; set; }
        public string Shelfnumber { get; set; }
        public int Comid { get; set; }

        public string Status { get; set; }
    }
}