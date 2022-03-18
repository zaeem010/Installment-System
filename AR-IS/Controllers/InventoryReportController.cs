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
    public class InventoryReportController : Controller
    {
        private ApplicationDbContext _context;

        public InventoryReportController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: InventoryReport
        public ActionResult Index(Product Product)
        {
            var ViewModel = new InventoryVM
            {
                Cat_list = _context.Database.SqlQuery<Category>("SELECT  * FROM   Categories WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Product=Product
            };
            return View(ViewModel);
        }
        public ActionResult SearchReport(Product Product ,string  s_date, string e_date ,string R_type)
        {
            List<InventoryVMQ> Inventory_list = new List<InventoryVMQ>();
            List<OpeningVMQ> OpeningStock = new List<OpeningVMQ>();
            if (Product.Cid == 0)
            {
                OpeningStock = _context.Database.SqlQuery<OpeningVMQ>("SELECT Id, Iname, StockIn - StockOut AS Opening FROM (SELECT Id, Iname, Openingstock + StockIn AS StockIn, StockOut FROM (SELECT Id, Iname, Itemunit, Openingstock, PurCTN * Itemunit + PurQty AS StockIn, SaleCTN * Itemunit + SaleQty AS StockOut FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty, PurCTN, PurCTN * Itemunit AS TPurQty, SaleQty, SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date+"')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')) AS derivedtbl_1) AS derivedtbl_2) AS derivedtbl_3) AS derivedtbl_4").ToList();
                Inventory_list = _context.Database.SqlQuery<InventoryVMQ>("SELECT Id, Iname,Itemunit, Openingstock,PurQty,PurCTN ,SaleQty,SaleCTN FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty,PurCTN ,PurCTN * Itemunit AS TPurQty, SaleQty,SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date+"' AND '"+e_date+"')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            }
            else 
            {
                OpeningStock = _context.Database.SqlQuery<OpeningVMQ>("SELECT Id, Iname, StockIn - StockOut AS Opening FROM (SELECT Id, Iname, Openingstock + StockIn AS StockIn, StockOut FROM (SELECT Id, Iname, Itemunit, Openingstock, PurCTN * Itemunit + PurQty AS StockIn, SaleCTN * Itemunit + SaleQty AS StockOut FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty, PurCTN, PurCTN * Itemunit AS TPurQty, SaleQty, SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')   and (Cid='"+Product.Cid+"')   ) AS derivedtbl_1) AS derivedtbl_2) AS derivedtbl_3) AS derivedtbl_4").ToList();
                Inventory_list = _context.Database.SqlQuery<InventoryVMQ>("SELECT Id, Iname,Itemunit, Openingstock,PurQty,PurCTN ,SaleQty,SaleCTN FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty,PurCTN ,PurCTN * Itemunit AS TPurQty, SaleQty,SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')  AND  (Cid='"+Product.Cid+"')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            }
            var ViewModel = new InventoryVM
            {
                Cat_list = _context.Database.SqlQuery<Category>("SELECT  * FROM   Categories WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                Product = Product,
                s_date=s_date,
                e_date= e_date,
                R_type=R_type,
                Inventory_list=Inventory_list,
                OpeningStock= OpeningStock,
            };
            return View(ViewModel);
        }
        public ActionResult Print(int Cid, string s_date, string e_date, string R_type)
        {
            List<InventoryVMQ> Inventory_list = new List<InventoryVMQ>();
            List<OpeningVMQ> OpeningStock = new List<OpeningVMQ>();
            if (Cid == 0)
            {
                OpeningStock = _context.Database.SqlQuery<OpeningVMQ>("SELECT Id, Iname, StockIn - StockOut AS Opening FROM (SELECT Id, Iname, Openingstock + StockIn AS StockIn, StockOut FROM (SELECT Id, Iname, Itemunit, Openingstock, PurCTN * Itemunit + PurQty AS StockIn, SaleCTN * Itemunit + SaleQty AS StockOut FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty, PurCTN, PurCTN * Itemunit AS TPurQty, SaleQty, SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')) AS derivedtbl_1) AS derivedtbl_2) AS derivedtbl_3) AS derivedtbl_4").ToList();
                Inventory_list = _context.Database.SqlQuery<InventoryVMQ>("SELECT Id, Iname,Itemunit, Openingstock,PurQty,PurCTN ,SaleQty,SaleCTN FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty,PurCTN ,PurCTN * Itemunit AS TPurQty, SaleQty,SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            }
            else
            {
                OpeningStock = _context.Database.SqlQuery<OpeningVMQ>("SELECT Id, Iname, StockIn - StockOut AS Opening FROM (SELECT Id, Iname, Openingstock + StockIn AS StockIn, StockOut FROM (SELECT Id, Iname, Itemunit, Openingstock, PurCTN * Itemunit + PurQty AS StockIn, SaleCTN * Itemunit + SaleQty AS StockOut FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty, PurCTN, PurCTN * Itemunit AS TPurQty, SaleQty, SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date < '" + s_date + "')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')   and (Cid='" + Cid + "')   ) AS derivedtbl_1) AS derivedtbl_2) AS derivedtbl_3) AS derivedtbl_4").ToList();
                Inventory_list = _context.Database.SqlQuery<InventoryVMQ>("SELECT Id, Iname,Itemunit, Openingstock,PurQty,PurCTN ,SaleQty,SaleCTN FROM (SELECT Id, Iname, Openingstock, Itemunit, PurQty,PurCTN ,PurCTN * Itemunit AS TPurQty, SaleQty,SaleCTN, SaleCTN * Itemunit AS TSaleQty FROM (SELECT Id, Iname, CAST(ISNULL(Openingstock, 0) AS int) AS Openingstock, CAST(ISNULL(Itemunit, 0) AS int) AS Itemunit, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM PurDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM PurDetails AS PurDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS PurCTN, (SELECT CAST(ISNULL(SUM(Qty), 0) AS int) AS Expr1 FROM SaleDetails WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleQty, (SELECT CAST(ISNULL(SUM(CTN), 0) AS int) AS Expr1 FROM SaleDetails AS SaleDetails_1 WHERE (Products.Id = Itemid) AND (Comid = Products.Comid) AND (Date BETWEEN '" + s_date + "' AND '" + e_date + "')) AS SaleCTN FROM Products WHERE (Comid = '" + Session["Company"] + "') AND (Status = 'Active')  AND  (Cid='" + Cid + "')) AS derivedtbl_1) AS derivedtbl_2").ToList();
            }
            var ViewModel = new InventoryVM
            {
                Setting = _context.Database.SqlQuery<Setting>("SELECT  *    FROM   Settings  WHERE  (Comid = '" + Session["Company"] + "') ").FirstOrDefault(),
                Cat_list = _context.Database.SqlQuery<Category>("SELECT  * FROM   Categories WHERE (Comid = '" + Session["Company"] + "')").ToList(),
                s_date = s_date,
                e_date = e_date,
                R_type = R_type,
                Inventory_list = Inventory_list,
                OpeningStock = OpeningStock,
            };
            return View(ViewModel);
        }
    }
}