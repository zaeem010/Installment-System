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
    public class SecondlevelController : Controller
    {
        private ApplicationDbContext _context;

        public SecondlevelController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Secondlevel
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<SecondLevelVMQ>("SELECT Secondlevels.Id, Secondlevels.Headid, Secondlevels.SubHeadid, Secondlevels.AccountNo, Secondlevels.AccountTitle, Secondlevels.Comid, AccountHeads.Head_Name, Firstlevels.AccountTitle AS AccountName FROM Secondlevels INNER JOIN AccountHeads ON Secondlevels.Headid = AccountHeads.Id INNER JOIN Firstlevels ON Secondlevels.SubHeadid = Firstlevels.AccountNo WHERE(Secondlevels.Comid = '" + Session["Company"] + "') AND (Firstlevels.Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(Secondlevel Secondlevel)
        {
            var viewModel = new AccountVM
            {
                Ac_headlist = _context.tbl_Ac_Head.ToList(),
                Firstlevels = _context.Database.SqlQuery<Firstlevel>("SELECT * FROM   Firstlevels  WHERE (AccountNo = '-0921')").ToList(),
                Secondlevel = Secondlevel,
            };
            return View(viewModel);
        }
        public ActionResult ShowData(int message)
        {
            var lst = _context.Database.SqlQuery<Firstlevel>("SELECT * FROM Firstlevels WHERE(Headid = '" + message + "') AND (Comid  = '" + Session["Company"] + "') ").ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public int count;
        public int account_no;
        public int final_acc;
        public ActionResult Save(int SubHead, Secondlevel SecondLevel)
        {
            if (SecondLevel.Id == 0)
            {
                var Second = _context.Database.SqlQuery<int>("Select  Count(*) as count from SecondLevels where AccountTitle = '" + SecondLevel.AccountTitle + "' AND Comid= '" + Session["Company"] + "'").SingleOrDefault();
                var count = Second;
                if (count == 0)
                {
                    if (SecondLevel.Headid == 1)
                    {
                        SecondLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM SecondLevels where Headid = '1' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = SecondLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("1000001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (SecondLevel.Headid == 2)
                    {
                        SecondLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM SecondLevels where Headid = '2' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = SecondLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("2000001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (SecondLevel.Headid == 3)
                    {
                        SecondLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM SecondLevels where Headid = '3' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = SecondLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("3000001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (SecondLevel.Headid == 4)
                    {
                        SecondLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM SecondLevels where Headid = '4' AND Comid= '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = SecondLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("4000001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (SecondLevel.Headid == 5)
                    {
                        SecondLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM SecondLevels where Headid = '5' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = SecondLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("5000001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    _context.Database.ExecuteSqlCommand("insert into  SecondLevels (Headid, SubHeadid, AccountNo, AccountTitle, Comid  ) values(" + SecondLevel.Headid + "," + SubHead + "," + final_acc + ",N'" + SecondLevel.AccountTitle + "', '" + Session["Company"] + "')");


                    return RedirectToAction("New");
                }
                else
                {

                    return RedirectToAction("Index");
                }
            }
            else
            {
                _context.Database.ExecuteSqlCommand("UPDATE SecondLevels SET AccountTitle ='" + SecondLevel.AccountTitle + "' WHERE id='" + SecondLevel.Id + "'");
                return RedirectToAction("Index");
            }

        }


        public ActionResult Edit(int? id)
        {
            var Secondlevel = _context.tbl_Secondlevel.SingleOrDefault(c => c.Id == id);
            if (Secondlevel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = new AccountVM
            {
                AccountNo = _context.Database.SqlQuery<int>("SELECT AccountNo  FROM   Firstlevels WHERE (AccountNo = '" + Secondlevel.SubHeadid + "')").FirstOrDefault(),
                AccountTitle = _context.Database.SqlQuery<string>("SELECT AccountTitle FROM   Firstlevels WHERE (AccountNo = '" + Secondlevel.SubHeadid + "')").FirstOrDefault(),
                Secondlevel = Secondlevel,
                Ac_headlist = _context.tbl_Ac_Head.ToList(),
            };
            return View("New", viewModel);
        }


    }
}