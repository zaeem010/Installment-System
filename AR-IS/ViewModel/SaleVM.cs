using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class SaleVM
    {
        public IEnumerable<Customer> Cus_list { get; set; }
        public IEnumerable<Product> Prod_list { get; set; }
        public IEnumerable<SaleDetail> SaleDetail_list { get; set; }
        public SaleMaster SaleMaster { get; set; }
        public TranscationDetail TranscationDetail { get; set; }
        public Setting Setting { get; set; }
        public string wordsinum { get; set; }
        public PrintSaleVMQ PrintSale { get; set; }
        public IEnumerable<SaleVMQ> SaleRecent { get; set; }
    }
}