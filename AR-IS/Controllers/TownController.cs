using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    public class TownController : Controller
    {
        private ApplicationDbContext _context;

        public TownController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Town
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Town>("SELECT  * FROM   Towns"));
        }
        public ActionResult New(Town Town)
        {
            return View(Town);
        }
        public ActionResult Save(Town Town)
        {
            string vardirection = "";
            if (Town.Id == 0)
            {
                _context.tbl_Town.Add(Town);
                _context.SaveChanges();
                vardirection = "New";
                TempData["Reg"] = "Registered Successfully";
            }
            else
            {

                var Towndb = _context.tbl_Town.Single(c => c.Id == Town.Id);
                Towndb.Name = Town.Name;
                _context.SaveChanges();
                vardirection = "Index";
                TempData["Reg"] = " Update Successfully";
            }
            return RedirectToAction(vardirection, "Town");
        }
        public ActionResult Edit(int? id)
        {
            var Town = _context.tbl_Town.SingleOrDefault(c => c.Id == id);
            if (Town == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", Town);
        }
        public ActionResult Delete(int id)
        {
            var Town = _context.tbl_Town.SingleOrDefault(c => c.Id == id);
            _context.tbl_Town.Remove(Town);
            _context.SaveChanges();
            TempData["Reg1"] = "Data Delete Successfully";
            return RedirectToAction("Index", "Town");
        }
    }
}