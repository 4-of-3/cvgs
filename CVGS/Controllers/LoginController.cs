﻿using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class LoginController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["MemberId"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "UserName,Pwd")] LoginViewModel login)
        {
            ObjectParameter loginMemberId = new ObjectParameter("memberId", typeof(int));
            try
            {
                db.SP_MEMBER_LOGIN(login.UserName, login.Pwd, loginMemberId);
                int memberId = (int)loginMemberId.Value;
                Session["MemberId"] = memberId;
                return RedirectToAction("Index", "Account");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View(login);
        }
    }
}