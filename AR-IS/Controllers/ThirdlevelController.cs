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
    public class ThirdlevelController : Controller
    {
        private ApplicationDbContext _context;

        public ThirdlevelController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Thirdlevel
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<ThirdLevelVMQ>("SELECT ThirdLevels.Id, ThirdLevels.AccountNo, ThirdLevels.HeadId, ThirdLevels.FirstLevelId, ThirdLevels.SecondLevelId, ThirdLevels.AccountTitle, ThirdLevels.AccountType, ThirdLevels.Cr, ThirdLevels.Dr, ThirdLevels.Comid, AccountHeads.Head_Name, Firstlevels.AccountTitle AS FirstAccountTitle, Secondlevels.AccountTitle AS SecondAccountTitle FROM ThirdLevels INNER JOIN AccountHeads ON ThirdLevels.HeadId = AccountHeads.Id INNER JOIN Firstlevels ON ThirdLevels.FirstLevelId = Firstlevels.AccountNo INNER JOIN Secondlevels ON ThirdLevels.SecondLevelId = Secondlevels.AccountNo WHERE(ThirdLevels.Comid = '" + Session["Company"] + "') AND (Secondlevels.Comid = '" + Session["Company"] + "') AND (Firstlevels.Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(ThirdLevel ThirdLevel)
        {
            var viewModel = new AccountVM
            {
                Ac_headlist = _context.tbl_Ac_Head.ToList(),
                ThirdLevel = ThirdLevel
            };
            return View(viewModel);
        }

        public ActionResult ShowData(int message)
        {
            var lst = _context.Database.SqlQuery<Firstlevel>("SELECT * FROM Firstlevels WHERE(Headid = '" + message + "') AND (Comid = '" + Session["Company"] + "') ").ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Second(int message)
        {
            var lst = _context.Database.SqlQuery<Secondlevel>("SELECT * FROM Secondlevels WHERE(SubHeadid = '" + message + "') AND (Comid = '" + Session["Company"] + "') ").ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public int count;
        public int account_no;
        public int final_acc;

        public ActionResult Save(string SubHead, ThirdLevel ThirdLevel, string SecondHeadid)
        {
            if (ThirdLevel.Id == 0)
            {
                var ac_main = _context.Database.SqlQuery<int>("Select  Count(*) as count from ThirdLevels where AccountTitle = '" + ThirdLevel.AccountTitle + "' AND    Comid='" + Session["Company"] + "'  ").FirstOrDefault();
                var count = ac_main;
                if (count == 0)
                {
                    var AccountType = _context.Database.SqlQuery<string>("SELECT AccountTitle FROM Secondlevels WHERE(AccountNo = '" + SecondHeadid + "') AND (Comid='" + Session["Company"] + "')").SingleOrDefault();
                    if (ThirdLevel.HeadId == 1)
                    {
                        ThirdLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM ThirdLevels where HeadId = '1' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = ThirdLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("1100001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (ThirdLevel.HeadId == 2)
                    {
                        ThirdLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM ThirdLevels where HeadId = '2' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = ThirdLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("2200001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (ThirdLevel.HeadId == 3)
                    {
                        ThirdLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM ThirdLevels where HeadId = '3' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = ThirdLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("3300001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (ThirdLevel.HeadId == 4)
                    {
                        ThirdLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM ThirdLevels where HeadId = '4' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = ThirdLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("4400001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    if (ThirdLevel.HeadId == 5)
                    {
                        ThirdLevel.AccountNo = _context.Database.SqlQuery<int>("SELECT ISNULL(MAX(AccountNo), 0) as account_no FROM ThirdLevels where HeadId = '5' AND Comid=  '" + Session["Company"] + "'").FirstOrDefault();
                        account_no = ThirdLevel.AccountNo;
                        if (account_no == 0)
                        {
                            final_acc = Convert.ToInt32("5500001");
                        }
                        else
                        {
                            final_acc = account_no + 1;
                        }
                    }
                    _context.Database.ExecuteSqlCommand("insert into ThirdLevels (AccountNo, HeadId, FirstLevelId, SecondLevelId, AccountTitle, AccountType, Dr, Cr, Comid) values(" + final_acc + "," + ThirdLevel.HeadId + "," + SubHead + "," + SecondHeadid + ",N'" + ThirdLevel.AccountTitle + "',N'" + AccountType + "',0,0,'" + Session["Company"] + "')");
                    return RedirectToAction("New");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                _context.Database.ExecuteSqlCommand("UPDATE ThirdLevels SET AccountTitle ='" + ThirdLevel.AccountTitle + "' WHERE Id='" + ThirdLevel.Id + "'");
                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(int? Id)
        {
            var ThirdLevel = _context.tbl_ThirdLevel.SingleOrDefault(c => c.Id == Id);
            if (ThirdLevel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = new AccountVM
            {
                Ac_headlist = _context.tbl_Ac_Head.ToList(),
                firstlevel = _context.Database.SqlQuery<string>("SELECT AccountTitle FROM   Firstlevels WHERE (AccountNo = '" + ThirdLevel.FirstLevelId + "')").FirstOrDefault(),
                secondlevel = _context.Database.SqlQuery<string>("SELECT AccountTitle FROM   Secondlevels WHERE (AccountNo = '" + ThirdLevel.SecondLevelId + "')").FirstOrDefault(),
                ThirdLevel = ThirdLevel,
               

            };
            return View("New", viewModel);
        }
    }
}