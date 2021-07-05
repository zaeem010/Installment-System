using AR_IS.Models;
using AR_IS.ViewModelQuery;
using AR_IS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class TrailBalanceController : Controller
    {
        private ApplicationDbContext _context;

        public TrailBalanceController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: TrailBalance
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TrailBalanceSearch(string s_date, string e_date)
        {
            var Setting = _context.Database.SqlQuery<Setting>("SELECT * FROM Settings WHERE Comid= '" + Session["Company"] + "' ").FirstOrDefault();
            var trail = _context.Database.SqlQuery<TrailVMQ>("SELECT HeadId, AccountNo, AccountTitle, Cr, Dr, cdr, ccr FROM (SELECT HeadId, AccountNo, AccountTitle, Cr, Dr, cdr, ccr FROM (SELECT  HeadId, AccountNo, AccountTitle, Cr, Dr, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS cdr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TransactionDetails_1 WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (Comid = '" + Session["Company"] + "')) AS derivedtbl_2) AS derivedtbl_1").ToList();
            var TrailVM = new TrailVM
            {
                Setting = Setting,
                s_date = s_date,
                e_date = e_date,
                trail = trail,
            };
            return View(TrailVM);
        }
    }
}