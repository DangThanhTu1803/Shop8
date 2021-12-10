using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Controllers
{
    public class ProductController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            //var productDao = new ProductDao();
            //ViewBag.AllProducts = productDao.ListNewProduct(500);

            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll() ;
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult MainMenuCategory()
        {
            var model = new MenuDao().ListByGroupId();
            return PartialView(model);
        }

        public ActionResult Category(long id)
        {
            var category = new ProductCategoryDao().ViewDetail(id);
           // ViewBag.Category = category;
            return View(category);
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID);
            return View(product);
        }
    }
}