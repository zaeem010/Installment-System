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
    public class PayableReceivableController : Controller
    {
        private ApplicationDbContext _context;
        public PayableReceivableController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PayableReceivable
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string s_date)
        {
            var lst = _context.Database.SqlQuery<PayRecVMQ>("SELECT AccountNo, AccountName, rec1 + rec2 AS Receivable, pay1 + pay2 AS Payable FROM (SELECT TOP (100) PERCENT AccountNo, AccountTitle AS AccountName, Rec, Pay, CASE WHEN HeadId = 1 AND rec >= 0 THEN Rec ELSE 0 END AS rec1, CASE WHEN rec < 0 THEN Rec * - 1 ELSE 0 END AS pay1, CASE WHEN HeadId = 2 AND pay < 0 THEN pay * - 1 ELSE 0 END AS rec2, CASE WHEN pay >= 0 THEN pay ELSE 0 END AS pay2 FROM (SELECT HeadId, AccountNo, AccountTitle, Dr + drr AS sumdr, Cr + crr AS sumcr, CASE WHEN HeadId = 1 THEN dr + drr - cr - crr ELSE 0 END AS Rec, CASE WHEN HeadId = 2 THEN cr + crr - dr - drr ELSE 0 END AS Pay FROM (SELECT AccountNo, HeadId, AccountTitle, Cr, Dr, CASE WHEN HeadId = 1 THEN (SELECT ISNULL(SUM(Dr) - SUM(Cr), 0) AS Expr1 FROM dbo.TranscationDetails WHERE (AccountNo = AccountNo) AND (CONVERT(varchar(7), TransDate, 126) = '"+s_date+ "') AND Comid = '" + Session["Company"] + "') ELSE 0 END AS drr, CASE WHEN HeadId = 2 THEN (SELECT ISNULL(SUM(cr) - SUM(dr), 0) AS Expr1 FROM dbo.TranscationDetails WHERE (AccountNo = AccountNo) AND (CONVERT(varchar(7), TransDate, 126) = '" + s_date+ "') AND Comid = '" + Session["Company"] + "') ELSE 0 END AS crr FROM ThirdLevels WHERE (HeadId IN (1, 2)) AND (Comid = '" + Session["Company"] + "')) AS derivedtbl_1) AS derivedtbl_2 ORDER BY AccountNo) AS derivedtbl_3").ToList();
            var ViewModel = new PayRecVM
            {
                PayrecList = lst,
                s_date = Convert.ToDateTime(s_date).ToString("MMMM-yyyy"),
            };
            return View(ViewModel);
        }
    }
}