using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class SettingController : Controller
    {
        private ApplicationDbContext _context;


        public SettingController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Setting
        public ActionResult Update()
        {
            var Setting = _context.Database.SqlQuery<Setting>("SELECT * FROM Settings WHERE Comid= '" + Session["Company"] + "'").SingleOrDefault();
            return View("Update", Setting);
        }
        public ActionResult Save(Setting Setting, HttpPostedFileBase img)
        {
            string vardirection = "";
            string ImageName = "";
            string ImageName2 = "";
            string physicalpath;

            if (img != null)
            {
                ImageName = System.IO.Path.GetFileName(img.FileName);
                physicalpath = Server.MapPath("~/uploads/" + ImageName);
                img.SaveAs(physicalpath);
            }
            
            if (Setting.Id > 0)
            {

                var Settingdb = _context.tbl_Setting.Single(c => c.Id == Setting.Id);
                ImageName2 = _context.Database.SqlQuery<string>("SELECT     Logo  FROM         Settings  where id=" + Setting.Id).FirstOrDefault();
                if (ImageName != "")
                {
                    ImageName2 = ImageName;
                }
                Settingdb.Companyname = Setting.Companyname;
                Settingdb.Phone = Setting.Phone;
                Settingdb.Address = Setting.Address;
                Settingdb.Logo = Setting.Logo;
                Settingdb.Landline = Setting.Landline;
                Settingdb.Email = Setting.Email;
                Settingdb.GST = Setting.GST;
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("update Settings set Logo='" + ImageName2 + "' where id=" + Setting.Id + ";");
                vardirection = "Update";
            }
            return RedirectToAction(vardirection, "Setting");
        }
    }
}