using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using QRCoder;
using System;
using System.Collections.Generic;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class LedgerController : Controller
    {
        private ApplicationDbContext _context;
        private List<LedgerQuery> trans_list;
        public LedgerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Ledger
        public ActionResult Index(ThirdLevel ThirdLevel)
        {
            var third_level = _context.Database.SqlQuery<ThirdLevel>("SELECT * FROM ThirdLevels WHERE (Comid = " + Session["Company"] + ")").ToList();
            var Ledger_VM = new LedgerVM
            {
                ThirdList = third_level,
                ThirdLevel = ThirdLevel,
            };
            return View(Ledger_VM);
        }
        [HttpPost]
        public ActionResult LedgerSearch(ThirdLevel ThirdLevel, string s_date, string e_date, TranscationDetail transactionDetail)
        {
            var third_level = _context.Database.SqlQuery<ThirdLevel>("SELECT * FROM ThirdLevels WHERE (Comid = " + Session["Company"] + ")").ToList();
            decimal opening_bal = _context.Database.SqlQuery<decimal>("SELECT isnull(sum(Dr - Cr),0) as opening FROM ThirdLevels WHERE AccountNo = " + ThirdLevel.AccountNo + " AND (Comid = " + Session["Company"] + ")").FirstOrDefault();
            decimal opening_bal1 = _context.Database.SqlQuery<decimal>("SELECT case When AccountNo Like '[15]%' Then ISNULL(sum(Dr) - sum(Cr),0) WHEN AccountNo Like '[234]%' Then isnull(sum(Cr) - sum(Dr),0) End As Opening FROM TranscationDetails WHERE AccountNo = '"+ThirdLevel.AccountNo+"' and TransDate < '"+s_date+ "' And (Comid = " + Session["Company"] + ") group by AccountNo  ").FirstOrDefault();
            ThirdLevel.AccountTitle = _context.Database.SqlQuery<string>("SELECT AccountTitle FROM ThirdLevels WHERE AccountNo = " + ThirdLevel.AccountNo + " AND (Comid = " + Session["Company"] + ")").FirstOrDefault();
            decimal sumDr = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(Dr), 0.00) AS Dr FROM  TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = '" + ThirdLevel.AccountNo + "') AND (Comid = " + Session["Company"] + ")").FirstOrDefault();
            decimal sumCr = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(Cr), 0.00) AS Cr FROM  TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = '" + ThirdLevel.AccountNo + "') And (Comid = " + Session["Company"] + ")").FirstOrDefault();

            if (ThirdLevel.AccountNo == 0)
            {
                trans_list = _context.Database.SqlQuery<LedgerQuery>("SELECT ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.BankAcc, TranscationDetails.V_No, ThirdLevels.Comid, TranscationDetails.Transid,TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE(TranscationDetails.TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.Comid = " + Session["Company"] + ")").ToList();

            }
            else if (ThirdLevel.AccountNo != 0 && s_date != "" && e_date != "")
            {
                trans_list = _context.Database.SqlQuery<LedgerQuery>("SELECT ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.BankAcc, TranscationDetails.V_No, ThirdLevels.Comid, TranscationDetails.Transid, TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE(TranscationDetails.TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.AccountNo = '" + ThirdLevel.AccountNo + "') AND (TranscationDetails.Comid = '" + Session["Company"] + "')").ToList();

            }
            else if (s_date == "" && e_date == "")
            {
                trans_list = _context.Database.SqlQuery<LedgerQuery>("SELECT ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.BankAcc, TranscationDetails.V_No, ThirdLevels.Comid, TranscationDetails.Transid, TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.AccountNo = '" + ThirdLevel.AccountNo + "') AND (TranscationDetails.Comid = '" + Session["Company"] + "')").ToList();

            }

            ThirdLevel.Cr = opening_bal + Convert.ToInt32(opening_bal1);
            var Ledger_VM = new LedgerVM
            {
                ThirdList = third_level,
                ThirdLevel = ThirdLevel,
                transactionDetail = transactionDetail,
                trans_list = trans_list,
                s_date = s_date,
                e_date = e_date,
                sumDr = sumDr,
                SumCr = sumCr
            };
            return View(Ledger_VM);
        }

        public ActionResult Print(ThirdLevel ThirdLevel, string s_date, string e_date, TranscationDetail transactionDetail)
        {
            var third_level = _context.Database.SqlQuery<ThirdLevel>("SELECT * FROM ThirdLevels WHERE (Comid = " + Session["Company"] + ")").ToList();
            decimal opening_bal = _context.Database.SqlQuery<decimal>("SELECT isnull(sum(Dr - Cr),0) as opening FROM ThirdLevels WHERE AccountNo = " + ThirdLevel.AccountNo + " AND (Comid = " + Session["Company"] + ")").FirstOrDefault();
            decimal opening_bal1 = _context.Database.SqlQuery<decimal>("SELECT isnull(sum(Dr - Cr),0) as opening FROM TranscationDetails WHERE AccountNo = " + ThirdLevel.AccountNo + " and TransDate < '" + s_date + "' And (Comid = " + Session["Company"] + ")").FirstOrDefault();
            ThirdLevel.AccountTitle = _context.Database.SqlQuery<string>("SELECT AccountTitle FROM ThirdLevels WHERE AccountNo = " + ThirdLevel.AccountNo + " AND (Comid = " + Session["Company"] + ")").FirstOrDefault();
            decimal sumDr = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(Dr), 0.00) AS Dr FROM  TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = '" + ThirdLevel.AccountNo + "') AND (Comid = " + Session["Company"] + ")").FirstOrDefault();
            decimal sumCr = _context.Database.SqlQuery<decimal>("SELECT ISNULL(SUM(Cr), 0.00) AS Cr FROM  TranscationDetails WHERE (TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (AccountNo = '" + ThirdLevel.AccountNo + "') And (Comid = " + Session["Company"] + ")").FirstOrDefault();

            if (ThirdLevel.AccountNo == 0)
            {
                trans_list = _context.Database.SqlQuery<LedgerQuery>("SELECT ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.BankAcc, TranscationDetails.V_No, ThirdLevels.Comid, TranscationDetails.Transid,TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE(TranscationDetails.TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.Comid = " + Session["Company"] + ")").ToList();

            }
            else if (ThirdLevel.AccountNo != 0 && s_date != "" && e_date != "")
            {
                trans_list = _context.Database.SqlQuery<LedgerQuery>("SELECT ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.BankAcc, TranscationDetails.V_No, ThirdLevels.Comid, TranscationDetails.Transid, TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE(TranscationDetails.TransDate BETWEEN '" + s_date + "' AND '" + e_date + "') AND (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.AccountNo = '" + ThirdLevel.AccountNo + "') AND (TranscationDetails.Comid = '" + Session["Company"] + "')").ToList();

            }
            else if (s_date == "" && e_date == "")
            {
                trans_list = _context.Database.SqlQuery<LedgerQuery>("SELECT ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.BankAcc, TranscationDetails.V_No, ThirdLevels.Comid, TranscationDetails.Transid, TranscationDetails.AccountNo FROM ThirdLevels INNER JOIN TranscationDetails ON ThirdLevels.AccountNo = TranscationDetails.AccountNo WHERE (ThirdLevels.Comid = " + Session["Company"] + ") AND (TranscationDetails.AccountNo = '" + ThirdLevel.AccountNo + "') AND (TranscationDetails.Comid = '" + Session["Company"] + "')").ToList();

            }

            ThirdLevel.Cr = opening_bal + Convert.ToInt32(opening_bal1);
            var Ledger_VM = new LedgerVM
            {
                ThirdList = third_level,
                ThirdLevel = ThirdLevel,
                transactionDetail = transactionDetail,
                trans_list = trans_list,
                s_date = s_date,
                e_date = e_date,
                sumDr = sumDr,
                SumCr = sumCr
            };
            return View(Ledger_VM);
        }


        

    }
}