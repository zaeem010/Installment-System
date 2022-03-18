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
    public class ProvinceController : Controller
    {
        private ApplicationDbContext _context;

        public ProvinceController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Province
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Province>("SELECT  * FROM   Provinces"));
        }
        public ActionResult New(Province Province)
        {
            return View(Province);
        }
        public ActionResult Save(Province Province)
        {
            string vardirection = "";
            if (Province.Id == 0)
            {
                _context.tbl_Province.Add(Province);
                _context.SaveChanges();
                vardirection = "New";
                TempData["Reg"] = "Registered Successfully";
            }
            else
            {
                var Provincedb = _context.tbl_Province.Single(c => c.Id == Province.Id);
                Provincedb.Name = Province.Name;
                _context.SaveChanges();
                vardirection = "Index";
                TempData["Reg"] = " Update Successfully";
            }
            return RedirectToAction(vardirection, "Province");
        }
        public ActionResult Edit(int? id)
        {
            var Province = _context.tbl_Province.SingleOrDefault(c => c.Id == id);
            if (Province == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", Province);
        }
        public ActionResult Delete(int id)
        {
            var Province = _context.tbl_Province.SingleOrDefault(c => c.Id == id);
            _context.tbl_Province.Remove(Province);
            _context.SaveChanges();
            TempData["Reg1"] = "Data Delete Successfully";
            return RedirectToAction("Index", "Province");
        }
    }
}