using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comname { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Town { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string CNIC { get; set; }

    }
}