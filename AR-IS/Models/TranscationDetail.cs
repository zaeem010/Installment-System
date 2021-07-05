using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class TranscationDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Transid { get; set; }
        public string TransDes { get; set; }
        public string TransDate { get; set; }
        public int AccountNo { get; set; }
        public decimal Dr { get; set; }
        public decimal Cr { get; set; }
        public int Invid { get; set; }
        public string Vtype { get; set; }
        public string cle_date { get; set; }
        public string check_no { get; set; }
        public string Bank { get; set; }
        public string BankDes { get; set; }
        public string Remarks { get; set; }
        public string BankAcc { get; set; }
        public int V_No { get; set; }
        public int Comid { get; set; }
    }
}