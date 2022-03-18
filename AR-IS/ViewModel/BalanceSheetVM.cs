using AR_IS.Models;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class BalanceSheetVM
    {
        public IEnumerable<BalanceSheetVMQ> Assest { get; set; }
        public IEnumerable<BalanceSheetVMQ> Liabilty { get; set; }
        public IEnumerable<BalanceSheetVMQ> Capital { get; set; }
        public Setting Setting { get; set; }
        public string s_date { get; set; }
        public string e_date { get; set; }
    }
}