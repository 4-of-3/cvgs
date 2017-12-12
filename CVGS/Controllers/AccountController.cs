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
using System.Data.Entity.Core.Objects;

namespace CVGS.Controllers
{
    public class AccountController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: MEMBERs
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Find and display the member profile
            MEMBER member = db.MEMBERs.Find(memberId);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: MEMBERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MEMBERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FName,LName,Email,UserName,Pwd,PwdConfirm")] NewAccountViewModel account)
        {
            if (db.MEMBERs.Where(m=>m.UserName == account.UserName).Count() != 0)
            {
                ModelState.AddModelError("UserName", "That user name is already in use.");
            }
            // Validate and create the member account
            if (ModelState.IsValid)
            {
                try
                {
                    db.SP_ADD_MEMBER(account.FName, account.LName, account.UserName, account.Email, account.Pwd, null, null, null, null );

                    var member = db.MEMBERs.Find(db.MEMBERs.Max(m => m.MemberId));
                    Session["MemberId"] = member.MemberId;
                    Session["MemberRole"] = member.ROLE.RoleName;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.GetBaseException().Message);
                }
            }

            return View(account);
        }

        // GET: MEMBERs/Edit/5
        public ActionResult Edit(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the member for profile editing
            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            
            // Use ViewModel for display purposes
            EditAccountViewModel account = new EditAccountViewModel()
            {
                MemberId = member.MemberId,
                FName = member.FName,
                LName = member.LName,
                UserName = member.UserName,
                Email = member.Email,
                FavPlatform = member.FavPlatform,
                FavCategory = member.FavCategory,
                FavGame = member.FavGame,
                FavQuote = member.FavQuote
            };
            return View(account);
        }

        // POST: MEMBERs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FName,LName,Email,FavPlatform,FavCategory,FavGame,FavQuote,Address")] EditAccountViewModel account)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and update the member's profile
            if (ModelState.IsValid)
            {
                var member = db.MEMBERs.Find(account.MemberId);

                if (member == null)
                {
                    return HttpNotFound();
                }

                member.FName = account.FName;
                member.LName = account.LName;
                member.Email = account.Email;
                member.FavCategory = account.FavCategory;
                member.FavPlatform = account.FavPlatform;
                member.FavGame = account.FavGame;
                member.FavQuote = account.FavQuote;

                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: MEMBERs/Delete/5
        public ActionResult Delete(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the member and display for deletion
            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            // Use a ViewModel to display custom fields for deletion confirmation
            DeleteAccountViewModel account = new DeleteAccountViewModel()
            {
                MemberId = member.MemberId,
                UserName = member.UserName,
                FullDelete = false
            };

            return View(account);
        }

        // POST: MEMBERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "MemberId,UserName,FullDelete")]DeleteAccountViewModel account)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Find the member for deletion
            MEMBER member = db.MEMBERs.Find(memberId);

            if (member == null)
            {
                return HttpNotFound();
            }

            // Accounts can be deleted or deactivated
            if (account.FullDelete)
            {
                db.MEMBERs.Remove(member);
                db.SaveChanges();
            }
            else
            {
                member.ActiveStatus = false;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();

            }

            // Logout the user
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        // GET: Password Change
        public ActionResult ChangePassword()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Find and display the member profile
            MEMBER member = db.MEMBERs.Find(memberId);
            if (member == null)
            {
                return HttpNotFound();
            }
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel()
            {
                MemberId = (int)memberId,
                OldPwd = "",
                NewPwd = "",
                NewPwdCheck = ""
            };
            
            return View(changePasswordViewModel);
        }

        // POST:
        [HttpPost, ActionName("ChangePassword")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "MemberId,OldPwd,NewPwd,NewPwdCheck")]ChangePasswordViewModel account)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // Find the member for deletion
            MEMBER member = db.MEMBERs.Find(memberId);

            if (member == null)
            {
                return HttpNotFound();
            }
            ObjectParameter newPassword = new ObjectParameter("newPwd", typeof(byte));

            //member.Pwd = newPassword;
            db.Entry(member).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Account");
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
