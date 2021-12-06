using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Categories()
        {
            var productDao = new ProductDao();
            ViewBag.AllProducts = productDao.ListNewProduct(24);

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
            return View();
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID);
            return View(product);
        }
    }
}