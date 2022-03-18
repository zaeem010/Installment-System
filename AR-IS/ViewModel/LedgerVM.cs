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
        public Setting Setting { get; set; }
        public IEnumerable<PurDetail> PurDetail { get; set; }
        public IEnumerable<PurDetailVehicle> PurDetailVehicle { get; set; }
        public IEnumerable<PurDetailReturn> PurDetailReturn { get; set; }
        public IEnumerable<SaleDetail> SaleDetail { get; set; }
        public IEnumerable<SWI> SWI { get; set; }
        public IEnumerable<SaleDetailReturn> SaleDetailReturn { get; set; }
    }
}