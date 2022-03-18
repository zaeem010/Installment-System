using AR_IS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class BrandController : Controller
    {
        private ApplicationDbContext _context;

        public BrandController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Brand

        public ActionResult New(Brand Brand)
        {
            return View(Brand);
        }
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Brand>("SELECT  Id, Name, Comid  FROM   Brands  WHERE (Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult Save(Brand Brand)
        {
            string varDirection = "";
            if (Brand.Id == 0)
            {
                _context.tbl_Brand.Add(Brand);
                Brand.Comid = Convert.ToInt32(Session["Company"]);
                varDirection = "New";
                TempData["Reg"] = "Registered Successfully";
            }
            else
            {
                var Branddb = _context.tbl_Brand.Single(c => c.Id == Brand.Id);
                Branddb.Name = Brand.Name;
                varDirection = "Index";
                TempData["Reg"] = " Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "Brand");
        }
        public ActionResult Edit(int id)
        {
            var Brand = _context.tbl_Brand.SingleOrDefault(c => c.Id == id);
            if (Brand == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", Brand);
        }
        public ActionResult Delete(int id)
        {
            var Brand = _context.tbl_Brand.SingleOrDefault(b => b.Id == id);
            _context.tbl_Brand.Remove(Brand);
            _context.SaveChanges();
            TempData["Reg1"] = "Data Delete Successfully";
            return RedirectToAction("Index");
        }
    }
}