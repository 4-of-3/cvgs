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
            var memberId = this.Session["MemberId"];
            // Redirect unauthenticated members
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

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
        public ActionResult Create([Bind(Include = "FName,LName,Email,UserName,Pwd,FavPlatform,FavCategory,FavGame,FavQuote")] NewAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter newMemberId = new ObjectParameter("memberId", typeof(int));
                try
                {
                    db.SP_ADD_MEMBER(account.FName, account.LName, account.UserName, account.Email, account.Pwd, account.FavPlatform, account.FavCategory, account.FavGame, account.FavQuote);
                    var memberId = db.MEMBERs.Max(m => m.MemberId);
                    Session["MemberId"] = memberId;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.GetBaseException());
                }
            }

            return View(account);
        }

        // GET: MEMBERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER account = db.MEMBERs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: MEMBERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FName,LName,UserName,Email,FavPlatform,FavCategory,FavGame,FavQuote,DateJoined,ActiveStatus,Pwd")] MEMBER account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: MEMBERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
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
            int memberId = account.MemberId;
            MEMBER member = db.MEMBERs.Find(memberId);
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
            Session.Clear();
            return RedirectToAction("Index", "Login");
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
