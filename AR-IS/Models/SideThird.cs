using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class SideThird
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int Formid { get; set; }
        public int SubFormid { get; set; }
        public int SuperFormid { get; set; }
        public string Name { get; set; }
    }
}