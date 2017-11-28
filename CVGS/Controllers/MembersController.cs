using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;
using CVGS.ViewModels;

namespace CVGS.Controllers
{
    public class MembersController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Members
        public ActionResult Index(string sort, string order)
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

            bool asc = true;
            ViewBag.listSortAsc = "asc";
            if (order != null && order.Equals("asc"))
            {
                ViewBag.listSortAsc = "desc";
                asc = true;
            }
            else if (order != null && order.Equals("desc"))
            {
                ViewBag.listSortAsc = "asc";
                asc = false;
            }

            if (sort != null)
            {
                switch (sort)
                {
                    case "username":
                        members = asc
                            ? members.OrderBy(e => e.UserName).ToList()
                            : members.OrderByDescending(e => e.UserName).ToList();
                        break;
                    case "name":
                        members = asc
                            ? members.OrderBy(e => e.FName).ToList()
                            : members.OrderByDescending(e => e.FName).ToList();
                        break;
                    case "email":
                        members = asc
                            ? members.OrderBy(e => e.Email).ToList()
                            : members.OrderByDescending(e => e.Email).ToList();
                        break;
                }
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

            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        public ActionResult Role(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if((string)Session["MemberRole"] != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            MEMBER member = db.MEMBERs.Find(id);
            UpdateRoleViewModel memberRole = new UpdateRoleViewModel()
            {
                MemberId = member.MemberId,
                RoleId = (int)member.RoleId,
                Roles = new SelectList(db.ROLEs,"RoleId","RoleName", (int)member.RoleId)
            };

            return View(memberRole);
        }

        [HttpPost]
        public ActionResult Role([Bind(Include = "MemberId, RoleId")] UpdateRoleViewModel memberRole)
        {
            if (ModelState.IsValid)
            {
                MEMBER member = db.MEMBERs.Find(memberRole.MemberId);
                member.RoleId = memberRole.RoleId;

                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberRole);
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
