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
    public class OpeningBalanceController : Controller
    {
        private ApplicationDbContext _context;

        public OpeningBalanceController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: OpeningBalance
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<ThirdLevel>("SELECT  * FROM   ThirdLevels WHERE (Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult OpenBalance(int? ID, ThirdLevel ThirdLevel)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ThirdLevels = _context.tbl_ThirdLevel.SingleOrDefault(c => c.AccountNo == ID);
            var ViewModel = new AccountVM
            {

                ThirdLevel = ThirdLevels,
                ThirdLevellist = _context.Database.SqlQuery<ThirdLevel>("SELECT  * FROM   ThirdLevels WHERE (Comid = '" + Session["Company"] + "')").ToList(),
            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult OpenBalance(ThirdLevel ThirdLevel)

        {
            _context.Database.ExecuteSqlCommand("UPDATE  ThirdLevels SET Cr ='" + ThirdLevel.Cr + "', Dr ='" + ThirdLevel.Dr + "' WHERE Id='" + ThirdLevel.Id + "'");
            return RedirectToAction("Index");
        }
    }
}