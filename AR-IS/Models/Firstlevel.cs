using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class Firstlevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Headid { get; set; }
        public string AccountTitle { get; set; }
        public int Comid { get; set; }
    }
}