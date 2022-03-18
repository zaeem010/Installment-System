using AR_IS.Models;
using AR_IS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class ReferenceController : Controller
    {
        private ApplicationDbContext _context;

        public ReferenceController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Reference
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<References>("SELECT * FROM    [References]  WHERE(Comid = '" + Session["Company"] + "') ").ToList());

        }
        public ActionResult New(References References)
        {
            var viewModel = new ReferenceVM
            {
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                References = References,
            };
            return View(viewModel);
        }
        public ActionResult Save(References References,string CNIC, ThirdLevel Thirdlevel, HttpPostedFileBase img)
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
            if (References.id == 0)
            {
                References.Image = ImageName;
                References.CNIC = CNIC;
                _context.tbl_References.Add(References);
                References.Comid = Convert.ToInt32(Session["Company"]);
                _context.SaveChanges();
                
                vardirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else
            {
                var Referencesdb = _context.tbl_References.Single(c => c.id == References.id);
                ImageName2 = _context.Database.SqlQuery<string>("SELECT     Image FROM   References  where id='" + References.id + "'   AND Comid='" + Session["Company"] + "'").FirstOrDefault();
                if (ImageName != "")
                {
                    ImageName2 = ImageName;
                }
                Referencesdb.Name = References.Name;
                Referencesdb.Phone1 = References.Phone1;
                Referencesdb.Phone2 = References.Phone2; 
                Referencesdb.Phone3 = References.Phone3;
                Referencesdb.Email = References.Email;
                Referencesdb.Image = References.Image;
                Referencesdb.Address = References.Address;
                Referencesdb.Town = References.Town;
                Referencesdb.Province = References.Province;
                Referencesdb.CNIC = CNIC;
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("update Referencess set Image='" + ImageName2 + "' where id='" + References.id + "'  AND Comid='" + Session["Company"] + "'");
                vardirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            return RedirectToAction(vardirection, "Reference");
        }
        public ActionResult Edit(int? id)
        {
            var References = _context.Database.SqlQuery<References>("SELECT     *   FROM   References  where AccountNo='" + id + "' AND Comid='" + Session["Company"] + "' ").FirstOrDefault();
            if (References == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = new ReferenceVM
            {
                Province_list = _context.Database.SqlQuery<Province>("SELECT * FROM   Provinces").ToList(),
                Town_list = _context.Database.SqlQuery<Town>("SELECT * FROM   Towns  ").ToList(),
                References = References
            };
            return View("New", viewModel);
        }

    }
}