using AR_IS.Models;
using AR_IS.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class UserManController : Controller
    {
        private ApplicationDbContext _context;

        public UserManController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: UserMan
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<GeneralUser>("SELECT   *  FROM    GeneralUsers  where Comid='" + Session["Company"] + "' and id not in ('" + Session["UserID"] + "')").ToList());
        }
        public ActionResult New(GeneralUser GeneralUser)
        {
            var FormsMenus = _context.tbl_SideFirst.ToList();
            var FormsSubMenus = _context.tbl_SideSecond.ToList();
            var FormsSuperSubMenus = _context.tbl_SideThird.ToList();
            var ViewModel = new LoginVM
            {
                FormsMenus = FormsMenus,
                FormsSubMenus = FormsSubMenus,
                FormsSuperSubMenus = FormsSuperSubMenus,
                GeneralUser = GeneralUser

            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult GetJavaScriptString(string ChildNodes, string username, GeneralUser GeneralUser ,string Phone, string Email,string Pass)
        {
            int COUNT = _context.Database.SqlQuery<int>("SELECT   COUNT(UserName) AS Expr1  FROM    GeneralUsers   WHERE   (UserName = '" + GeneralUser.UserName + "')").FirstOrDefault();
            if (COUNT == 1)
            {
                TempData["Reg"] = " '"+username+"' Already Registered";
                return RedirectToAction("New", "UserMan");
            }
            else
            {
                var RegDate = _context.Database.SqlQuery<DateTime>("SELECT  RegDate  FROM  GeneralUsers WHERE        (Comid = '" + Session["Company"] + "') AND (Id = '" + Session["UserID"] + "')").FirstOrDefault();
                var ExpDate = _context.Database.SqlQuery<DateTime>("SELECT        ExpDate  FROM            GeneralUsers WHERE        (Comid = '" + Session["Company"] + "') AND (Id = '" + Session["UserID"] + "')").FirstOrDefault();
                _context.Database.ExecuteSqlCommand(" INSERT INTO GeneralUsers (UserName, Email, Password, Phone, [Plan], RegDate, ExpDate, Comid) VALUES  ('" + username + "','" + Email + "','" + Pass + "','" + Phone + "','14 days free trial','" + RegDate + "','" + ExpDate + "','" + Session["Company"] + "') ");
                string[] aListItems = ChildNodes.Split(',');
                var count = aListItems.Count();
                if (aListItems != null)
                {
                    for (int i = 0; i < aListItems.Count(); i++)
                    {
                        if (Convert.ToInt32(aListItems[i]) <= Convert.ToInt32("1000"))
                        {
                            var value = aListItems[i];
                            _context.Database.ExecuteSqlCommand("INSERT INTO UserAccesses     (Form_id, SubForm_id, SuperForm_id, Comid, Username) values('" + value + "','0','0','" + Session["Company"] + "','" + username + "')");
                        }
                        else if (Convert.ToInt32(aListItems[i]) > Convert.ToInt32("1000") & Convert.ToInt32(aListItems[i]) < Convert.ToInt32("10000"))
                        {
                            var value = aListItems[i];
                            _context.Database.ExecuteSqlCommand("INSERT INTO UserAccesses     (Form_id, SubForm_id, SuperForm_id, Comid, Username) values('0','" + value + "','0','" + Session["Company"] + "','" + username + "')");
                        }
                        else if (Convert.ToInt32(aListItems[i]) > Convert.ToInt32("10000"))
                        {
                            var value = aListItems[i];
                            _context.Database.ExecuteSqlCommand("INSERT INTO UserAccesses (Form_id, SubForm_id, SuperForm_id, Comid, Username) values('0','0','" + value + "','" + Session["Company"] + "','" + username + "')");
                        }
                    }
                }
                TempData["Reg1"] = " '" + username + "' Registered  Successfully";
                return RedirectToAction("Index", "UserMan");
            }
        }
        public JsonResult chkPrevUser(string username)
        {
            var prevUser = _context.tbl_GeneralUser.Where(p => p.UserName == username).FirstOrDefault();

            if (prevUser == null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}