using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Controllers
{
    public class HomeController : Controller
    {


        // GET: Home
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(8);
            ViewBag.FeatureProducts = productDao.ListFeatureProduct(4);
            return View();
        }

        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId();
            return PartialView(model);
        }
    }
}