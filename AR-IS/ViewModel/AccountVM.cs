using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class AccountVM
    {
        //First Level // Second level // Third level 
        public IEnumerable<AccountHead> Ac_headlist { get; set; }
        public Firstlevel Firstlevel { get; set; }
        public IEnumerable<Firstlevel> Firstlevels { get; set; }
        public IEnumerable<Secondlevel> Secondlevels { get; set; }
        public Secondlevel Secondlevel { get; set; }
        public string AccountTitle { get; set; }
        public int AccountNo { get; set; }
        public ThirdLevel ThirdLevel { get; set; }
        public string firstlevel { get; set; }
        public string secondlevel { get; set; }
       
        
        public IEnumerable<ThirdLevel> ThirdLevellist { get; set; }
    }
}