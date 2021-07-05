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
    public class ExpenseSheetController : Controller
    {
        private ApplicationDbContext _context;

        public ExpenseSheetController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: ExpenseSheet
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<TranscationVMQ>("SELECT TranscationDetails.*, ThirdLevels.AccountType, ThirdLevels.AccountTitle FROM TranscationDetails INNER JOIN ThirdLevels ON TranscationDetails.AccountNo = ThirdLevels.AccountNo WHERE (TranscationDetails.Vtype = 'ESV') AND (TranscationDetails.Dr > 0)  AND (TranscationDetails.Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(TranscationDetail TranscationDetail)
        {

            var VoucherVM = new VoucherVM
            {
                Accounts = _context.Database.SqlQuery<ThirdLevel>("SELECT *  FROM   ThirdLevels  WHERE   (FirstLevelId = 5002) AND (Comid = '" + Session["Company"] + "')").ToList(),
                Transcation = _context.Database.SqlQuery<TranscationVMQ>("SELECT TranscationDetails.*, ThirdLevels.AccountType, ThirdLevels.AccountTitle FROM TranscationDetails INNER JOIN ThirdLevels ON TranscationDetails.AccountNo = ThirdLevels.AccountNo WHERE (TranscationDetails.Vtype = 'ESV') AND (TranscationDetails.Dr > 0) AND (TranscationDetails.V_No = -99) AND (TranscationDetails.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
            };
            return View(VoucherVM);
        }

        public ActionResult Save(TranscationDetail TranscationDetail,int[] accountno, decimal[] amount, string[] remarks, string date)
        {
                for (int i = 0; i < accountno.Count(); i++)
                {
                    var accname = _context.Database.SqlQuery<string>("SELECT  AccountTitle FROM ThirdLevels WHERE (AccountNo='" + accountno[i] + "') AND (Comid = '" + Session["Company"] + "')  ").SingleOrDefault();
                    var Narr = ("(" + accname + ") " + remarks[i] + "").ToString();
                    if (amount[i] != 0)
                    {
                        int Trans_id = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.Transid),0)+1 from TranscationDetails  where  (Comid = '" + Session["Company"] + "')  ").FirstOrDefault();
                        int V_No = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.V_No),0)+1 from TranscationDetails where VType = 'ESV'").FirstOrDefault();
                        _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid,TransDate,TransDes,AccountNo,Dr,Cr,Invid,Vtype,V_No,Comid) VALUES (" + Trans_id + ",'" + date + "',N'" + Narr + "'," + accountno[i] + "," + amount[i] + ",0,0,'ESV','" + V_No + "','" + Session["Company"] + "')");
                        _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid,TransDate,TransDes,AccountNo,Dr,Cr,Invid,Vtype,V_No,Comid) VALUES (" + Trans_id + ",'" + date + "',N'" + Narr + "','1100001','0','" + amount[i] + "',0,'ESV','" + V_No + "','" + Session["Company"] + "')");
                    }
                }
            TempData["Reg"] = "Data Submitted Successfully";
            return RedirectToAction("New", "ExpenseSheet");
        }
        public ActionResult Update(TranscationDetail TranscationDetail, int accountno, decimal amount, string remarks, string date , int id)
        {
               _context.Database.ExecuteSqlCommand("DELETE  FROM    TranscationDetails  WHERE    (Comid = '" + Session["Company"] + "') AND (Vtype = 'ESV') AND (V_No = '" + id + "')");
               var accname = _context.Database.SqlQuery<string>("SELECT  AccountTitle FROM ThirdLevels WHERE (AccountNo='" + accountno + "') AND (Comid = '" + Session["Company"] + "')  ").SingleOrDefault();
                var Narr = ("(" + accname + ") " + remarks + "").ToString();
                if (amount != 0)
                {
                    int Trans_id = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.Transid),0)+1 from TranscationDetails  where  (Comid = '" + Session["Company"] + "')  ").FirstOrDefault();
                    int V_No = _context.Database.SqlQuery<int>("select ISNULL(Max(TranscationDetails.V_No),0)+1 from TranscationDetails where VType = 'ESV'").FirstOrDefault();
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid,TransDate,TransDes,AccountNo,Dr,Cr,Invid,Vtype,V_No,Comid) VALUES (" + Trans_id + ",'" + date + "',N'" + Narr + "'," + accountno + "," + amount + ",0,0,'ESV','" + V_No + "','" + Session["Company"] + "')");
                    _context.Database.ExecuteSqlCommand("INSERT INTO TranscationDetails (Transid,TransDate,TransDes,AccountNo,Dr,Cr,Invid,Vtype,V_No,Comid) VALUES (" + Trans_id + ",'" + date + "',N'" + Narr + "','1100001','0','" + amount + "',0,'ESV','" + V_No + "','" + Session["Company"] + "')");
                }
            TempData["Reg"] = "Data Update Successfully";
            return RedirectToAction("Index", "ExpenseSheet");
        }
        public ActionResult Edit(int V_No , TranscationDetail  TranscationDetail)
        {
            var VoucherVM = new VoucherVM
            {
                Transcation = _context.Database.SqlQuery<TranscationVMQ>("SELECT TranscationDetails.*, ThirdLevels.AccountType, ThirdLevels.AccountTitle FROM TranscationDetails INNER JOIN ThirdLevels ON TranscationDetails.AccountNo = ThirdLevels.AccountNo WHERE (TranscationDetails.Vtype = 'ESV') AND (TranscationDetails.Dr > 0) AND (TranscationDetails.V_No = '"+V_No+"') AND (TranscationDetails.Comid = '" + Session["Company"] + "')").FirstOrDefault(),
                
            };
            return View("New",VoucherVM);
            
        }
        public ActionResult Delete(int V_No)
        {
            _context.Database.ExecuteSqlCommand("DELETE  FROM    TranscationDetails  WHERE    (Comid = '" + Session["Company"] + "') AND (Vtype = 'ESV') AND (V_No = '" + V_No + "')");
            TempData["Reg1"] = "Data Update Successfully";
            return RedirectToAction("Index");

        }
    }
}