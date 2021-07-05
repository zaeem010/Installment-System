using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Email { get; set; }
        public string NTN { get; set; }
        public string GST { get; set; }
        public string Town { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string CNIC { get; set; }
    }
}