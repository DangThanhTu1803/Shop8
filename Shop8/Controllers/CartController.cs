using Model.Dao;

using Shop8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Model.EF;
using Common;
using System.Configuration;

namespace Shop8.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult Update(String cartModel)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsoncart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });  
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;

            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productId,int quantity)
        {
            var product = new ProductDao().GetByID(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            decimal total = 0;
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                foreach (var item in list)
                {
                    var orderdetail = new OrderDetail();
                    
                    total += (item.Product.Price * item.Quantity);
                }
            }
            
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string phone, string address)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = phone;
            order.ShipName = shipName;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.ProductID = item.Product.ID;
                    orderdetail.OrderID = id;
                    orderdetail.Price = item.Product.Price;
                    orderdetail.Quantity = item.Quantity;
                    detailDao.Insert(orderdetail);

                    total += (item.Product.Price * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                
                new MailHelper().SentMail(toEmail,"Đơn hàng mới từ Shop8",content);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Redirect("/hoan-thanh");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}