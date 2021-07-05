using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class InstallmentPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string year { get; set; }
        public int MarkUp { get; set; }
        public string Detail { get; set; }
        public int Comid { get; set; }
    }
}