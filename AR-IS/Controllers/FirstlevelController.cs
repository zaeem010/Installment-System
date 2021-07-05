using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class FirstlevelController : Controller
    {
        private ApplicationDbContext _context;

        public FirstlevelController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Firstlevel
        public ActionResult New(Firstlevel Firstlevel)
        {
            var viewModel = new AccountVM
            {
                Ac_headlist = _context.tbl_Ac_Head.ToList(),
                Firstlevel = Firstlevel

            };
            return View(viewModel);
        }
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<FirstlevelVMQ>("SELECT Firstlevels.Id, Firstlevels.AccountNo, Firstlevels.Headid, Firstlevels.AccountTitle, AccountHeads.Head_Name, Firstlevels.Comid FROM Firstlevels INNER JOIN AccountHeads ON Firstlevels.Headid = AccountHeads.Id WHERE(Firstlevels.Comid = '" + Session["Company"] + "')").ToList());
        }
        public int count;
        public int account_no;
        public int final_acc;
        public ActionResult Save(Firstlevel FirstLevel)
        {

            if (FirstLevel.Id == 0)
            {
                var ac_main = _context.Database.SqlQuery<int>("Select  Count(*) as count from FirstLevels where AccountTitle  = '" + FirstLevel.AccountTitle + "'   And  Comid = '" + Session["Company"] + "' ").FirstOrDefault();
                var count = ac_main;
                if (count == 0)
                {
                    if (FirstLevel.Headid == 1)
                    {
                        FirstLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM FirstLevels where Headid = '1' And  Comid = '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = FirstLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("1001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (FirstLevel.Headid == 2)
                    {
                        FirstLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM FirstLevels where Headid = '2' And  Comid = '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = FirstLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("2001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (FirstLevel.Headid == 3)
                    {
                        FirstLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM FirstLevels where Headid = '3'  And  Comid = '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = FirstLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("3001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (FirstLevel.Headid == 4)
                    {
                        FirstLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM FirstLevels where Headid = '4'  And  Comid = '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = FirstLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("4001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (FirstLevel.Headid == 5)
                    {
                        FirstLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM FirstLevels where Headid = '5'  And  Comid = '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = FirstLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("5001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    
                    _context.Database.ExecuteSqlCommand("insert into  Firstlevels(AccountNo, Headid, AccountTitle, Comid) values (" + final_acc + "," + FirstLevel.Headid + ",N'" + FirstLevel.AccountTitle + "','" + Session["Company"] + "')");
                    return RedirectToAction("New");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                _context.Database.ExecuteSqlCommand("UPDATE FirstLevels SET AccountTitle ='" + FirstLevel.AccountTitle + "' WHERE id='" + FirstLevel.Id + "'");
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            var Firstlevel = _context.tbl_Firstlevel.SingleOrDefault(c => c.Id == id);
            if (Firstlevel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var AccountHeads = _context.tbl_Ac_Head.ToList();
            var viewModel = new AccountVM
            {
                Ac_headlist = AccountHeads,
                Firstlevel = Firstlevel
            };
            return View("New", viewModel);
        }


    }
}