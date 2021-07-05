using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class SideSecond
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Formid { get; set; }
        public int SubFormid { get; set; }
        public string Name { get; set; }
    }
}