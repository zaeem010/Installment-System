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
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Category
        public ActionResult New(Category Category)
        {
            return View(Category);
        }
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<Category>("SELECT  Id, Name, Comid  FROM   Categories  WHERE (Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult Save(Category Category)
        {
            string varDirection = "";
            if (Category.Id == 0)
            {
                _context.tbl_Category.Add(Category);
                Category.Comid = Convert.ToInt32(Session["Company"]);
                varDirection = "New";
                TempData["Reg"] = "Registered Successfully";
            }
            else
            {
                var Categorydb = _context.tbl_Category.Single(c => c.Id == Category.Id);
                Categorydb.Name = Category.Name;
                varDirection = "Index";
                TempData["Reg"] = " Update Successfully";
            }
            _context.SaveChanges();
            return RedirectToAction(varDirection, "Category");
        }
        public ActionResult Edit(int id)
        {
            var Category = _context.tbl_Category.SingleOrDefault(c => c.Id == id);
            if (Category == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("New", Category);
        }
        public ActionResult Delete(int id)
        {
            var Category = _context.tbl_Category.SingleOrDefault(b => b.Id == id);
            _context.tbl_Category.Remove(Category);
            _context.SaveChanges();
            TempData["Reg1"] = "Data Delete Successfully";
            return RedirectToAction("Index");
        }
    }
}