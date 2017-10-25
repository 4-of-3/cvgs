using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;

namespace CVGS.Controllers
{
    public class MembersController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Members
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            if (Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<MEMBER> members;
            if ((string)Session["MemberRole"] == "Admin")
            {
                members = db.MEMBERs.ToList();
            }
            else
            {
                // Exclude inactive members from the list
                members = db.MEMBERs.Where(m => m.ActiveStatus).ToList();
            }
            return View(members);
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == (int)Session["MemberId"])
            {
                //Member is looking at their own details page; redirect to account
                return RedirectToAction("Index", "Account");
            }

            MEMBER mEMBER = db.MEMBERs.Find(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
