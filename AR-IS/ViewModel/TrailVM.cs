using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_IS.Models;
using AR_IS.ViewModelQuery;

namespace AR_IS.ViewModel
{
    public class TrailVM
    {
        public string s_date { get; set; }
        public string e_date { get; set; }
        public IEnumerable<TrailVMQ> trail { get; set; }
        public Setting Setting { get; set; }
        public ThirdLevel ThirdLevel { get; set; }
        public IEnumerable<AccountHead> AccountHead { get; set; }
    }
}
