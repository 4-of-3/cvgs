using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class HomeController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,Pwd")] LoginViewModel login)
        {
            //var memberId = db.SP_MEMBER_LOGIN(login.UserName, login.Pwd, sqlDBType);
            ObjectParameter objectParameter = new ObjectParameter("memberId", typeof(int));
            db.SP_MEMBER_LOGIN(login.UserName, login.Pwd, objectParameter);
            var memberId = objectParameter.Value;
            this.Session["MemberId"] = memberId;
            return RedirectToAction("Index","Account");
        }
    }
}