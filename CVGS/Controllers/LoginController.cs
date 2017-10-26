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
            if (Session["MemberId"] != null)
            {
                // TODO: This has been replaced by "/Logout" route (make sure it isn't used anymore)
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            // Remove the user session (to remove authentication) and redirect to the Login page
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "UserName,Pwd")] LoginViewModel login)
        {
            ObjectParameter loginMemberId = new ObjectParameter("memberId", typeof(int));
            try
            {
                // Attempt to authenticate the member and redirect to the Dashboard
                db.SP_MEMBER_LOGIN(login.UserName, login.Pwd, loginMemberId);
                int memberId = (int)loginMemberId.Value;
                Session["MemberId"] = memberId;
                string memberRole = db.MEMBERs.Find(memberId).ROLE.RoleName;
                Session["MemberRole"] = memberRole;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View(login);
        }
    }
}