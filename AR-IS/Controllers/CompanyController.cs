using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    public class CompanyController : Controller
    {
        private ApplicationDbContext _context;

        public CompanyController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Company
        public ActionResult Index()
        {
            return View(_context.tbl_Company.ToList());
        }
        public ActionResult New(Company Company)
        {
            return View(Company);
        }
        public ActionResult Save(Company Company )
        {
            string varDirection = "";
            if (Company.Id == 0)
            {
                Company.Comid = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(CAST(Comid AS int)), 0) + 1 AS Comid  FROM   Companies").FirstOrDefault();
                _context.tbl_Company.Add(Company);
                _context.Database.ExecuteSqlCommand("INSERT  INTO Settings(Comid) VALUES ('"+Company.Comid+"')");
                varDirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else

            {
                var Companydb = _context.tbl_Company.Single(c => c.Id == Company.Id);
                Companydb.Name = Company.Name;

                varDirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "Branch");
        }
        public ActionResult Edit(int id)
        {
            var Company = _context.tbl_Company.SingleOrDefault(c => c.Id == id);
            if (Company == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", Company);
        }
    }
}