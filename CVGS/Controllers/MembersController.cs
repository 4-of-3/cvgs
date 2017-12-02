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
        public ActionResult Index(string search, string sort, string order)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can view members
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to view members");
            }

            // Members can be filtered and sorted
            List<MEMBER> members;
            if ((string)Session["MemberRole"] == "Admin")
            {
                members = db.MEMBERs.ToList();
            }
            else
            {
                // Non-admins can only see active members
                members = db.MEMBERs.Where(m => m.ActiveStatus).ToList();
            }

            // Only filter the list if a search term is specified
            if (search != null)
            {
                string tempSearch = search.ToLower();
                members = members.FindAll(x => x.UserName.ToLower().Contains(tempSearch) || x.FName.ToLower().Contains(tempSearch) || x.LName.ToLower().Contains(tempSearch) || x.Email.ToLower().Contains(tempSearch));
                ViewBag.search = search;
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

            if (sort == null) return View(members);

            // Handle list sorting
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
                case "role":
                    members = asc
                        ? members.OrderBy(e => e.ROLE.RoleName).ToList()
                        : members.OrderByDescending(e => e.ROLE.RoleName).ToList();
                    break;
            }

            return View(members);
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can view members
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to view members");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // When members view their own account they should be redirected to their profile page
            if (id == (int)Session["MemberId"])
            {
                return RedirectToAction("Index", "Account");
            }

            // Find and display member details
            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        public ActionResult Role(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins can change a member's role
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin")
            {
                return new HttpUnauthorizedResult("You are not authorized to change member roles");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find and display the member for role editing
            MEMBER member = db.MEMBERs.Find(id);
            UpdateRoleViewModel accountRole = new UpdateRoleViewModel()
            {
                MemberId = member.MemberId,
                RoleId = (int)member.RoleId,
                Roles = new SelectList(db.ROLEs,"RoleId","RoleName", (int)member.RoleId)
            };

            return View(accountRole);
        }

        [HttpPost]
        public ActionResult Role([Bind(Include = "MemberId, RoleId")] UpdateRoleViewModel accountRole)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins can change a member's role
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin")
            {
                return new HttpUnauthorizedResult("You are not authorized to change member roles");
            }

            // Validate and update the member's role
            if (ModelState.IsValid)
            {
                MEMBER member = db.MEMBERs.Find(accountRole.MemberId);
                if (member == null)
                {
                    return HttpNotFound();
                }

                member.RoleId = accountRole.RoleId;

                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = member.MemberId });
            }

            return View(accountRole);
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
