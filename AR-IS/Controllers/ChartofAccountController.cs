using AR_IS.Models;
using AR_IS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class ChartofAccountController : Controller
    {
        private ApplicationDbContext _context;

        public ChartofAccountController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: ChartofAccount
        public ActionResult Index()
        {
            var viewModel = new AccountVM
            {
                Ac_headlist= _context.tbl_Ac_Head.ToList(),
                Firstlevels = _context.Database.SqlQuery<Firstlevel>("SELECT * FROM   Firstlevels WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Secondlevels = _context.Database.SqlQuery<Secondlevel>("SELECT * FROM   Secondlevels  WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                ThirdLevellist=_context.Database.SqlQuery<ThirdLevel>("SELECT * FROM   ThirdLevels  WHERE (Comid = '" + Session["Company"] + "')").ToList(),
            };
            return View(viewModel);
        }
    }
}