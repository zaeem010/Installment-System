using AR_IS.Models;
using AR_IS.ViewModel;
using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
       
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View(_context.Database.SqlQuery<ProductVMQ>("SELECT Products.Id, Products.Shelfnumber, Products.Iname, Products.Cprice, Products.Sprice, Products.Itemunit, Products.Openingstock, Products.Barcode, Products.Reorderlevel, Products.Image, Products.Comid, Brands.Name AS Brand, Categories.Name AS Category, MeasuringUnits.Name AS UnitName FROM Products INNER JOIN Brands ON Products.Bid = Brands.Id INNER JOIN Categories ON Products.Cid = Categories.Id INNER JOIN MeasuringUnits ON Products.MeasuringUnit = MeasuringUnits.Id WHERE (Products.Comid = '" + Session["Company"] + "') AND (Categories.Comid = '" + Session["Company"] + "') AND (Brands.Comid = '" + Session["Company"] + "') AND (MeasuringUnits.Comid = '" + Session["Company"] + "')").ToList());
        }
        public ActionResult New(Product Product)
        {
            var Categories = _context.Database.SqlQuery<Category>("SELECT  * FROM   Categories WHERE (Comid = '" + Session["Company"] + "')").ToList();
            var Brands = _context.Database.SqlQuery<Brand>("SELECT  * FROM   Brands WHERE (Comid ='" + Session["Company"] + "')").ToList();
            var Units = _context.Database.SqlQuery<MeasuringUnit>("SELECT  * FROM   MeasuringUnits  WHERE (Comid ='" + Session["Company"] + "')").ToList();
            var ViewModel = new ProductVM
            {
                Category_list = Categories,
                Brand_list = Brands,
                Unit_list= Units,
                Product = Product
            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Save(HttpPostedFileBase[] file, Product Product, string[] IName, decimal[] cprice,decimal[] Itemunit, string [] shelfnumber, decimal[] sprice, int[] unitid, decimal[] openingstock, string[] barcode, string[] reorderlevel, string I_Names, HttpPostedFileBase img_1, string c_prices, string s_prices, string unitids, string opening_stocks, string barcodes, string reorder_levels, string Shelfnumbers,string Itemunits)
        {
            string vardirection = "";
            Random r = new Random();
            int num = r.Next();
            string ImageName = "";
            string physicalPath;
            string img;
            if (Product.Id == 0)
            {
                for (int i = 0; i < IName.Count(); i++)
                {
                    if (file[i] == null)
                    {
                        img = "demo.jpg";
                    }
                    else
                    {
                        ImageName = System.IO.Path.GetFileName(file[i].FileName);
                        img = num + ImageName;
                        physicalPath = Server.MapPath("~/uploads/" + img);
                        file[i].SaveAs(physicalPath);
                    }
                    _context.Database.ExecuteSqlCommand("INSERT INTO  Products( Bid, Cid, Iname, Cprice, Sprice,Itemunit ,Openingstock, MeasuringUnit, Barcode, Reorderlevel, Image,Shelfnumber ,Comid) VALUES ('" + Product.Bid+ "','" + Product.Cid + "','" + IName[i] + "','" + cprice[i] + "','" + sprice[i] + "','" + Itemunit[i] + "','" + openingstock[i] + "','" + unitid[i] + "','" + barcode[i] + "','" + reorderlevel[i] + "','" + img + "','"+ shelfnumber [i]+ "','" + Session["Company"] + "')");
                }
                vardirection = "New";
                TempData["Reg"] = "Data Submitted Successfully";
            }
            else if (img_1 != null)
            {
                ImageName = System.IO.Path.GetFileName(img_1.FileName);
                img = num + ImageName;
                physicalPath = Server.MapPath("~/uploads/" + img);
                img_1.SaveAs(physicalPath);
                _context.Database.ExecuteSqlCommand("UPDATE  Products SET  Bid ='" + Product.Bid + "', Cid ='" + Product.Cid + "', Iname ='" + I_Names + "', Cprice ='" + c_prices + "', Sprice ='" + s_prices + "',Itemunit='"+ Itemunits + "' ,Openingstock ='" + opening_stocks + "', MeasuringUnit ='" + Product.MeasuringUnit + "', Barcode ='" + barcodes + "', Reorderlevel ='" + reorder_levels + "', Image ='" + img + "', Shelfnumber ='"+Shelfnumbers+"' where (Id='" + Product.Id + "')");
                _context.SaveChanges();
                vardirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }
            else
            {
                _context.Database.ExecuteSqlCommand("UPDATE  Products SET  Bid ='" + Product.Bid + "', Cid ='" + Product.Cid + "', Iname ='" + I_Names + "', Cprice ='" + c_prices + "', Sprice ='" + s_prices + "',Itemunit='"+ Itemunits +"', Openingstock ='" + opening_stocks + "', MeasuringUnit ='" + Product.MeasuringUnit + "', Barcode ='" + barcodes + "', Reorderlevel ='" + reorder_levels + "' ,Shelfnumber ='" + Shelfnumbers + "' where (Id='" + Product.Id + "')");
                _context.SaveChanges();
                vardirection = "Index";
                TempData["Reg"] = "Data Update Successfully";
            }

            _context.SaveChanges();
            return RedirectToAction(vardirection, "Product");
        }
        public ActionResult Edit(int? id)
        {

            var Product = _context.Database.SqlQuery<Product>("SELECT  * FROM   Products  WHERE (Id = '" + id + "') AND (Comid = '" + Session["Company"] + "')").FirstOrDefault();
            if (Product == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var Categories = _context.Database.SqlQuery<Category>("SELECT  * FROM   Categories WHERE (Comid = '" + Session["Company"] + "')");
            var Brands = _context.Database.SqlQuery<Brand>("SELECT  * FROM   Brands WHERE (Comid = '" + Session["Company"] + "')");
            var Product_list = _context.Database.SqlQuery<Product>("SELECT  * FROM   Products  WHERE (Id = '" + id + "') AND (Comid = '" + Session["Company"] + "')").ToList();
            var Units = _context.Database.SqlQuery<MeasuringUnit>("SELECT  * FROM   MeasuringUnits  WHERE (Comid ='" + Session["Company"] + "') ").ToList();
            var ViewModel = new ProductVM
            {
                Category_list = Categories,
                Brand_list = Brands,
                Product_list = Product_list,
                Unit_list= Units,
                Product = Product
               
            };

            return View("New", ViewModel);
        }
        public ActionResult Delete(int id)
        {
            var Product = _context.tbl_Product.SingleOrDefault(b => b.Id == id );
            _context.tbl_Product.Remove(Product);
            _context.SaveChanges();
            TempData["Reg1"] = "Data Delete Successfully";
            return RedirectToAction("Index", "Product");
        }

        

    }
}

