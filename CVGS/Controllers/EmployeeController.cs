using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Make Page Accessible to Employees and Admins only
            if ((string)Session["MemberRole"] != "Employee" && (string)Session["MemberRole"] != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View();
        }
    }
}