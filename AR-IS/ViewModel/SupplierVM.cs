using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class SupplierVM
    {
        public IEnumerable<Town> Town_list { get; set; }
        public IEnumerable<Province> Province_list { get; set; }
        public Supplier Supplier { get; set; }
       
    }
}