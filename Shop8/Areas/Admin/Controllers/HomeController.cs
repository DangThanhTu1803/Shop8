﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.Thang = DateTime.Now.Month;
            ViewBag.Nam = DateTime.Now.Year;
            

            return View();
        }

    }
}