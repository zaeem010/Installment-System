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
    public class CargoController : Controller
    {
        private ApplicationDbContext _context;

        public CargoController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Cargo

        public ActionResult New(Cargo Cargo)
        {
            return View(Cargo);
        }
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Cargo>("SELECT  Id, Name, Comid  FROM   Cargoes  WHERE (Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult Save(Cargo Cargo)
        {
            string varDirection = "";
            if (Cargo.Id == 0)
            {
                _context.tbl_Cargo.Add(Cargo);
                Cargo.Comid = Convert.ToInt32(Session["Company"]);
                varDirection = "New";
                TempData["Reg"] = "Registered Successfully";
            }
            else
            {
                var Cargodb = _context.tbl_Cargo.Single(c => c.Id == Cargo.Id);
                Cargodb.Name = Cargo.Name;
                varDirection = "Index";
                TempData["Reg"] = " Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "Cargo");
        }
        public ActionResult Edit(int id)
        {
            var Cargo = _context.tbl_Cargo.SingleOrDefault(c => c.Id == id);
            if (Cargo == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", Cargo);
        }
    }
}