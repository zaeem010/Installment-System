using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR_IS.Models;
namespace AR_IS.ViewModel
{
    public class LoginVM
    {
        public string alert { get; set; }
        public Userlogin Userlogin { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public GeneralUser GeneralUser { get; set; }
        public List<SideFirst> FormsMenus { get; set; }
        public List<SideSecond> FormsSubMenus { get; set; }
        public List<SideThird> FormsSuperSubMenus { get; set; }
        public IEnumerable<GeneralUser> GeneralUser_list { get; set; }
    }
}