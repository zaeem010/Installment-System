using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class BalanceSheetController : Controller
    {

        private ApplicationDbContext _context;

        public BalanceSheetController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: BalanceSheet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BalanceSheetSearch(string s_date, string e_date)
        {
            var Setting = _context.Database.SqlQuery<Setting>("SELECT * FROM Settings WHERE Comid= '" + Session["Company"] + "' ").FirstOrDefault();
            var Assests = _context.Database.SqlQuery<BalanceSheetVMQ>("SELECT AccountNo, AccountTitle, cdr, ccr FROM (SELECT AccountNo, AccountTitle, Cr, Dr, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS cdr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TransactionDetails_1 WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (HeadId = 1) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_2 ").ToList();
            var Liabilty = _context.Database.SqlQuery<BalanceSheetVMQ>("SELECT AccountNo, AccountTitle, cdr, ccr FROM (SELECT AccountNo, AccountTitle, Cr, Dr, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS cdr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TransactionDetails_1 WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (HeadId = 2) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_2 ").ToList();
            var Capital = _context.Database.SqlQuery<BalanceSheetVMQ>("SELECT AccountNo, AccountTitle, cdr, ccr FROM (SELECT AccountNo, AccountTitle, Cr, Dr, (SELECT ISNULL(SUM(Dr), 0) AS Expr1 FROM TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS cdr, (SELECT ISNULL(SUM(Cr), 0) AS Expr1 FROM TranscationDetails AS TransactionDetails_1 WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = ThirdLevels.AccountNo) AND (Comid = ThirdLevels.Comid)) AS ccr FROM ThirdLevels WHERE (HeadId = 3) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_2 ").ToList();
            var BalanceSheetVM = new BalanceSheetVM
            {
                s_date = s_date,
                e_date = e_date,
                Setting = Setting,
                Assest = Assests,
                Liabilty = Liabilty,
                Capital = Capital,
            };
            return View(BalanceSheetVM);
        }
    }
}