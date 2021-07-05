using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class UserDetailsController : Controller
    {
        private ApplicationDbContext _context;
        public UserDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: UserDetails
        public ActionResult Index()
        {
            var UserDetails = _context.tbl_GeneralUser.ToList().Where(c=>c.UserName != "SuperAdmin");
            return View(UserDetails);
        }
        public ActionResult Update( GeneralUser GeneralUser)
        {
            return View(GeneralUser);
        }
        public ActionResult User_Update(GeneralUser GeneralUser)
        {
            string varDirection = "";

            var GeneralUserdb = _context.tbl_GeneralUser.Single(c => c.Id == GeneralUser.Id);
            GeneralUserdb.UserName = GeneralUser.UserName;
            GeneralUserdb.Email = GeneralUser.Email;
            GeneralUserdb.Phone = GeneralUser.Phone;
            GeneralUserdb.RegDate = GeneralUser.RegDate;
            GeneralUserdb.ExpDate = GeneralUser.ExpDate;
            varDirection = "Index";
            TempData["Reg"] = "Data Update Successfully";
            _context.SaveChanges();
            return RedirectToAction(varDirection, "UserDetails");
        }
        public ActionResult Edit(int id)
        {
            var UserDetails = _context.tbl_GeneralUser.SingleOrDefault(c => c.Id == id);
            if (UserDetails == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("Update", UserDetails);
        }
    }
}