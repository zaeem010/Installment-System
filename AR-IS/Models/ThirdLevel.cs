using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class ThirdLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int HeadId { get; set; }
        public int FirstLevelId { get; set; }
        public int SecondLevelId { get; set; }
        public string AccountTitle { get; set; }
        public string AccountType { get; set; }
        public decimal Cr { get; set; }
        public decimal Dr { get; set; }
        public int Comid { get; set; }
    }
}