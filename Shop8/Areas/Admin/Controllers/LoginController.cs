﻿using Model.Dao;
using Shop8.Areas.Admin.Models;
using Shop8.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop8.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                if (result==1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.UserID = user.ID;
                    userSession.Name = user.Name;
                    userSession.GroupID = user.GroupID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Bạn không phải Admin");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}