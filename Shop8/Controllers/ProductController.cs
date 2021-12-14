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
            return View();
        }
        public ActionResult Categories()
        {
            //var productDao = new ProductDao();
            //ViewBag.AllProducts = productDao.ListNewProduct(24);
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult MainMenuCategory()
        {
            var model = new MenuDao().ListByGroupId();
            return PartialView(model);
        }

        public ActionResult Category(long id, int page = 1, int pageSize = 12)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 1;
            int totalPage;
            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID);
            return View(product);
        }
    }
}