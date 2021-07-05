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
    public class MeasuringUnitController : Controller
    {
        private ApplicationDbContext _context;

        public MeasuringUnitController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: MeasuringUnit
        public ActionResult New(MeasuringUnit MeasuringUnit)
        {
            return View(MeasuringUnit);
        }
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<MeasuringUnit>("SELECT  Id, Name, Comid  FROM   MeasuringUnits  WHERE (Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult Save(MeasuringUnit MeasuringUnit)
        {
            string varDirection = "";
            if (MeasuringUnit.Id == 0)
            {
                _context.tbl_MeasuringUnit.Add(MeasuringUnit);
                MeasuringUnit.Comid = Convert.ToInt32(Session["Company"]);
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                var MeasuringUnitdb = _context.tbl_MeasuringUnit.Single(c => c.Id == MeasuringUnit.Id);
                MeasuringUnitdb.Name = MeasuringUnit.Name;
                varDirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "MeasuringUnit");
        }
        public ActionResult Edit(int id)
        {
            var MeasuringUnit = _context.tbl_MeasuringUnit.SingleOrDefault(c => c.Id == id);
            if (MeasuringUnit == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", MeasuringUnit);
        }
        public ActionResult Delete(int id)
        {
            var MeasuringUnit = _context.tbl_MeasuringUnit.SingleOrDefault(b => b.Id == id);
            _context.tbl_MeasuringUnit.Remove(MeasuringUnit);
            _context.SaveChanges();
            TempData["Reg1"] = "Data Delete Successfully";
            return RedirectToAction("Index");
        }
    }
}