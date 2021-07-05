using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class Setting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Companyname { get; set; }
        public string   Phone { get; set; }
        public string Landline { get; set; }
        public string  Email { get; set; }
        public string Address { get; set; }
        public string GST { get; set; }
        public string Logo { get; set; }
        public int Comid { get; set; }
    }
}