using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class ProductVM
    {
        public IEnumerable<Category> Category_list { get; set; }
        public IEnumerable<Brand> Brand_list { get; set; }
        public IEnumerable<MeasuringUnit> Unit_list { get; set; }
        public IEnumerable<Product> Product_list { get; set; }
        public Product Product { get; set; }
    }
}