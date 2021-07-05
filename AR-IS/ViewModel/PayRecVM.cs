using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR_IS.ViewModelQuery;

namespace AR_IS.ViewModel
{
    public class PayRecVM
    {
        public IEnumerable<PayRecVMQ> PayrecList { get; set; }
        public string s_date { get; set; }
    }
}