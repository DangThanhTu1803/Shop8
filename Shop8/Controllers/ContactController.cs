using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Infor = new ContactDao().ListAll();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Feedback model)
        {
            ViewBag.Infor = new ContactDao().ListAll();
            if (ModelState.IsValid)
            {
                var dao = new FeedbackDao();

                model.CreatedDate = DateTime.Now;
                var result = dao.Insert(model);
                if (result)
                {
                    ViewBag.Success = "Gửi phản hồi thành công!";
                }
                else
                {
                    ModelState.AddModelError("", "Gửi phản hồi không thành công!");
                }
            }
            return View(model);
        }
    }
}