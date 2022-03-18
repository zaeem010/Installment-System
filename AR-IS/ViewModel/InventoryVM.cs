using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR_IS.Models;
using AR_IS.ViewModelQuery;
namespace AR_IS.ViewModel
{
    public class InventoryVM
    {
        public IEnumerable <Category> Cat_list { get; set; }
        public IEnumerable<InventoryVMQ> Inventory_list { get; set; }
        public string s_date { get; set; }
        public string e_date { get; set; }
        public string R_type { get; set; }
        public Product Product { get; set; }
        public Setting Setting { get; set; }
        public IEnumerable<OpeningVMQ> OpeningStock { get; set; }
    }
}