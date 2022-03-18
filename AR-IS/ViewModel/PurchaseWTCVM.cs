using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class PurchaseWTCVM
    {
        public Supplier Supplier { get; set; }
        public IEnumerable<Supplier> Supp_list { get; set; }
        public IEnumerable<Product> Prod_list { get; set; }
        public IEnumerable<PurDetail> PurDetail_list { get; set; }
        public PurMaster PurMaster { get; set; }
        public TranscationDetail TranscationDetail { get; set; }
        public Setting Setting { get; set; }
        public PrintPurchaseVMQ PrintPurchase { get; set; }
        public string wordsinum { get; set; }
        public IEnumerable<PurchaseWTCVMQ> PurchaseRecent { get; set; }
        public IEnumerable<Town> Town_list { get; set; }
        public IEnumerable<Province> Province_list { get; set; }
        public IEnumerable<Cargo> Cargo_list { get; set; }
        public Cargo Cargo { get; set; }
    }
}