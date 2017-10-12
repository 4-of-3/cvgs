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
    public class LoginController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "UserName,Pwd")] LoginViewModel login)
        {
            //var memberId = db.SP_MEMBER_LOGIN(login.UserName, login.Pwd, sqlDBType);
            ObjectParameter objectParameter = new ObjectParameter("memberId", typeof(int));
            db.SP_MEMBER_LOGIN(login.UserName, login.Pwd, objectParameter);
            var memberId = objectParameter.Value;
            this.Session["MemberId"] = memberId;
            return RedirectToAction("Index", "Account");
        }
    }
}