using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AR_IS.Models
{
    public class UserAccessCopy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Form_id { get; set; }
        public int SubForm_id { get; set; }
        public int SuperForm_id { get; set; }
        public int Comid { get; set; }
        public string Username { get; set; }

    }
}