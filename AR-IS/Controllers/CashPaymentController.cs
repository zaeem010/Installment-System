using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class CashPaymentController : Controller
    {
        private ApplicationDbContext _context;

        public CashPaymentController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: CashPayment
        public ActionResult Index()
        {
           
            return View(_context.Database.SqlQuery<TranscationDetail>("SELECT * FROM TranscationDetails WHERE (Comid = '" + Session["Company"] + "') AND (Vtype = 'CPV') AND (Cr > 0) ORDER BY V_No DESC").ToList());
        }
        public ActionResult New(TranscationDetail TranscationDetail)
        {
            TranscationDetail.V_No = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.V_No),0)+1 from TranscationDetails where VType = 'CPV' AND (Comid= '" + Session["Company"] + "' ) AND(Invid='-1')").FirstOrDefault();
            var Acc_List = _context.Database.SqlQuery<ThirdLevel>("SELECT * FROM ThirdLevels  WHERE (Comid = '" + Session["Company"] + "') AND (AccountNo NOT IN (1100001,4400001,5500001,1100002))  ORDER BY AccountNo DESC ").ToList();
            var VoucherVM = new VoucherVM
            {
                TranscationDetail = TranscationDetail,
                Accounts = Acc_List,
            };
            return View(VoucherVM);
        }
        public ActionResult Save(TranscationDetail TranscationDetail, int[] account_no, decimal[] Dr, decimal[] Cr, string[] narr)
        {
            if (TranscationDetail.Id == 0)
            {
                TranscationDetail.Transid = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.Transid),0)+1 from TranscationDetails WHERE (Comid = '" + Session["Company"] + "')").FirstOrDefault();
                string paidforcash = ("(Cash)" + TranscationDetail.TransDes + "").ToString();
                for (int i = 0; i < account_no.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "',N'" + narr[i] + "','" + TranscationDetail.TransDate + "','" + account_no[i] + "','" + Dr[i] + "','" + Cr[i] + "','-1','CPV','" + TranscationDetail.V_No + "','" + Session["Company"] + "')");
                }
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','" + paidforcash + "','" + TranscationDetail.TransDate + "','1100001',0,'" + TranscationDetail.Cr + "','-1','CPV','" + TranscationDetail.V_No + "','" + Session["Company"] + "')");
                TempData["Reg"] = "Voucher Created Successfully";
                return RedirectToAction("New");
            }
            else
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails WHERE Vtype='CPV' AND (Comid = '" + Session["Company"] + "') AND V_No='" + TranscationDetail.V_No + "'");
                string paidforcash = ("" + TranscationDetail.TransDes + "").ToString();
                for (int i = 0; i < account_no.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "',N'" + narr[i] + "','" + TranscationDetail.TransDate + "','" + account_no[i] + "','" + Dr[i] + "','" + Cr[i] + "','-1','CPV','" + TranscationDetail.V_No + "','" + Session["Company"] + "')");
                }
                _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid, TransDes, TransDate, AccountNo, Dr, Cr, Invid, Vtype,V_No, Comid) VALUES ('" + TranscationDetail.Transid + "','" + paidforcash + "','" + TranscationDetail.TransDate + "','1100001',0,'" + TranscationDetail.Cr + "','-1','CPV','" + TranscationDetail.V_No + "','" + Session["Company"] + "')");
                TempData["Reg"] = "Voucher Update Successfully";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id, TranscationDetail TranscationDetail)
        {
            var TranscationsList = _context.Database.SqlQuery<TranscationVMQ>("SELECT TranscationDetails.Id, TranscationDetails.Transid, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.AccountNo, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Invid, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.Remarks, TranscationDetails.BankAcc, TranscationDetails.V_No, TranscationDetails.Comid , ThirdLevels.AccountTitle FROM TranscationDetails INNER JOIN ThirdLevels ON TranscationDetails.AccountNo = ThirdLevels.AccountNo WHERE(TranscationDetails.Comid = '" + Session["Company"] + "') AND (TranscationDetails.Vtype = 'CPV') AND (TranscationDetails.V_No = '"+id+"') AND (TranscationDetails.Dr > 0) AND (ThirdLevels.Comid = '" + Session["Company"] + "')").ToList();
            var Transcations = _context.Database.SqlQuery<TranscationDetail>("SELECT * FROM TranscationDetails WHERE(V_No = '" + id + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'CPV') AND (Cr > 0)").SingleOrDefault();
            var Acc_List = _context.Database.SqlQuery<ThirdLevel>("SELECT * FROM ThirdLevels  WHERE (Comid = '" + Session["Company"] + "') AND (AccountNo NOT IN (1100001,4400001,5500001,1100002))  ORDER BY AccountNo DESC ").ToList();
            var VoucherVM = new VoucherVM
            {
                TranscationDetail = Transcations,
                Accounts = Acc_List,
                TranscationList = TranscationsList,
            };
            return View("New", VoucherVM);

        }
        public ActionResult Delete(int id)
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TranscationDetails WHERE Vtype='CPV' AND (Comid = '" + Session["Company"] + "') AND V_No='" + id + "'");
            TempData["Reg1"] = "Voucher Deleted Successfully";
            return RedirectToAction("Index");
        }
        public ActionResult Report(int id, string Vtype, TranscationDetail Transaction_Detail)
        {
            var Setting = _context.Database.SqlQuery<Setting>("SELECT * FROM Settings WHERE Comid= '" + Session["Company"] + "'").FirstOrDefault();
            var TranscationsList = _context.Database.SqlQuery<TranscationVMQ>("SELECT TranscationDetails.Id, TranscationDetails.Transid, TranscationDetails.TransDes, TranscationDetails.TransDate, TranscationDetails.AccountNo, TranscationDetails.Dr, TranscationDetails.Cr, TranscationDetails.Invid, TranscationDetails.Vtype, TranscationDetails.cle_date, TranscationDetails.check_no, TranscationDetails.Bank, TranscationDetails.BankDes, TranscationDetails.Remarks, TranscationDetails.BankAcc, TranscationDetails.V_No, TranscationDetails.Comid, ThirdLevels.AccountTitle FROM TranscationDetails INNER JOIN ThirdLevels ON TranscationDetails.AccountNo = ThirdLevels.AccountNo WHERE(TranscationDetails.Comid = '" + Session["Company"] + "') AND (TranscationDetails.Vtype = 'CPV') AND (TranscationDetails.V_No = '" + id + "') AND (TranscationDetails.Dr > 0) AND (ThirdLevels.Comid = '" + Session["Company"] + "')").ToList();
            var Transcations = _context.Database.SqlQuery<TranscationDetail>("SELECT * FROM TranscationDetails WHERE(V_No = '" + id + "') AND (Comid = '" + Session["Company"] + "') AND (Vtype = 'CPV') AND (Cr > 0)").SingleOrDefault();
            var Acc_List = _context.Database.SqlQuery<ThirdLevel>("SELECT * FROM ThirdLevels WHERE (Comid = '" + Session["Company"] + "') ORDER BY AccountNo DESC ").ToList();
            decimal Dr = _context.Database.SqlQuery<decimal>("SELECT Cr FROM  TranscationDetails WHERE (Vtype = 'CPV') AND (Comid = '" + Session["Company"] + "') AND (V_No = '" + id + "') AND (Cr > 0)").SingleOrDefault();
            var wordsinum = NumberToWords(Decimal.ToInt32(Dr));
            var VoucherVM = new VoucherVM
            {
                Setting = Setting,
                TranscationDetail = Transcations,
                Accounts = Acc_List,
                TranscationList = TranscationsList,
                wordinnum = wordsinum,
            };
            return View(VoucherVM);
        }
        private object NumberToWords(string number)
        {
            throw new NotImplementedException();
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            string new_word = Regex.Replace(words, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return new_word;
        }
    }
}