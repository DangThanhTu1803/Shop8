using Model.Dao;
using Shop8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";

        // GET: Home
        public ActionResult Index()
        {
            var feedbackDao = new FeedbackDao();
            ViewBag.Comments = feedbackDao.ListComment(3);

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
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            var model = new MenuDao().ListByGroupId();
            return PartialView(model);
        }
    }
}