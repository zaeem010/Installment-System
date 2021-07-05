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
    public class InstallmentPlanController : Controller
    {
        private ApplicationDbContext _context;

        public InstallmentPlanController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: InstallmentPlan
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<InstallmentPlan>("SELECT  *  FROM   InstallmentPlans  WHERE (Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(InstallmentPlan InstallmentPlan)
        {
            return View(InstallmentPlan);
        }
        public ActionResult Save(InstallmentPlan InstallmentPlan)
        {
            string varDirection = "";
            if (InstallmentPlan.Id == 0)
            {
                _context.tbl_InstallmentPlan.Add(InstallmentPlan);
                InstallmentPlan.Comid = Convert.ToInt32(Session["Company"]);
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                var InstallmentPlandb = _context.tbl_InstallmentPlan.Single(c => c.Id == InstallmentPlan.Id);
                InstallmentPlandb.year = InstallmentPlan.year;
                InstallmentPlandb.MarkUp = InstallmentPlan.MarkUp;
                InstallmentPlandb.Detail = InstallmentPlan.Detail;

                varDirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "InstallmentPlan");
        }
        public ActionResult Edit(int id)
        {
            var InstallmentPlan = _context.tbl_InstallmentPlan.SingleOrDefault(c => c.Id == id);
            if (InstallmentPlan == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", InstallmentPlan);
        }
    }
}