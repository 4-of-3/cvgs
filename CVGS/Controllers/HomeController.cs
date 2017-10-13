using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Force unauthenticated users to sign in
            if (Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}