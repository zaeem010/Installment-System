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
    public class MonthlyExpenseController : Controller
    {
        private ApplicationDbContext _context;

        public MonthlyExpenseController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
        }
        // GET: MonthlyExpense
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Print(string month)
        {
            var VoucherVM = new VoucherVM
            {
                Profit =_context.Database.SqlQuery<decimal>("SELECT Sale - CGS AS Profit FROM (SELECT ISNULL(SUM(Cr), 0) AS Sale, ISNULL(SUM(Dr), 0) AS CGS FROM TranscationDetails WHERE (AccountNo IN (4400001, 5500001)) AND (Comid = " + Session["Company"] + ") AND (CONVERT(varchar(7), TransDate, 126) = '" + month+"')) AS derivedtbl_1").FirstOrDefault(),
                Month=month,
                MonthlyExpense=_context.Database.SqlQuery<MonthlyExpenseVMQ>("SELECT SUM(TranscationDetails.Dr) AS Expenses, ThirdLevels.SecondLevelId, ThirdLevels.AccountType FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.FirstLevelId = 5002) AND (ThirdLevels.Comid = 1) AND (TranscationDetails.Comid = " + Session["Company"] + ") AND (CONVERT(varchar(7), TranscationDetails.TransDate, 126) = '" + month+"') GROUP BY ThirdLevels.SecondLevelId, ThirdLevels.AccountType").ToList(),
            };
            return View(VoucherVM);
        }
        public ActionResult Details(string month ,int Ac)
        {
            var VoucherVM = new VoucherVM
            {
                MonthlyExpense = _context.Database.SqlQuery<MonthlyExpenseVMQ>("SELECT  ISNULL(SUM(TranscationDetails.Dr), 0) AS SumDr, ThirdLevels.AccountTitle, TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.SecondLevelId = '" + Ac + "') AND (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.Comid = " + Session["Company"] + ") AND (CONVERT(varchar(7), TranscationDetails.TransDate, 126) = '"+month+"') GROUP BY TranscationDetails.AccountNo, ThirdLevels.AccountTitle").ToList(),
                Month = month,
                
            };
            return View(VoucherVM);
        }
    }
}