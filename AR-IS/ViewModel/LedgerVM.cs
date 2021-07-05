using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;

namespace AR_IS.ViewModel
{
    public class LedgerVM
    {
        public ThirdLevel ThirdLevel { get; set; }
        public TranscationDetail transactionDetail { get; set; }
        public IEnumerable<ThirdLevel> ThirdList { get; set; }
        public IEnumerable<LedgerQuery> trans_list { get; set; }
        public IEnumerable<LedgerQuery> trans_in { get; set; }
        public string s_date { get; set; }
        public string e_date { get; set; }
        public decimal opening { get; set; }
        public decimal sumDr { get; set; }
        public decimal SumCr { get; set; }
    }
}