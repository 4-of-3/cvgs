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
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            MEMBER mEMBER = db.MEMBERs.Find(memberId);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
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
        public ActionResult Create([Bind(Include = "FName,LName,Email,UserName,Pwd,FavPlatform,FavCategory,FavGame,FavQuote")] NewAccountViewModel mEMBER)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter newMemberId = new ObjectParameter("memberId", typeof(int));
                db.SP_ADD_MEMBER(mEMBER.FName, mEMBER.LName, mEMBER.UserName, mEMBER.Email, mEMBER.Pwd, mEMBER.FavPlatform, mEMBER.FavCategory, mEMBER.FavGame, mEMBER.FavQuote);
                var memberId = db.MEMBERs.Max(m => m.MemberId);
                Session["MemberId"] = memberId;

                return RedirectToAction("Index");
            }

            return View(mEMBER);
        }

        // GET: MEMBERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = db.MEMBERs.Find(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
        }

        // POST: MEMBERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FName,LName,UserName,Email,FavPlatform,FavCategory,FavGame,FavQuote,DateJoined,ActiveStatus,Pwd")] MEMBER mEMBER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEMBER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mEMBER);
        }

        // GET: MEMBERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = db.MEMBERs.Find(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            DeleteAccountViewModel account = new DeleteAccountViewModel()
            {
                MemberId = mEMBER.MemberId,
                UserName = mEMBER.UserName,
                FullDelete = false
            };

            return View(account);
        }

        // POST: MEMBERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include ="MemberId,UserName,FullDelete")]DeleteAccountViewModel account)
        {
            int memberId = account.MemberId;
            MEMBER mEMBER = db.MEMBERs.Find(memberId);
            if (account.FullDelete)
            {
                db.MEMBERs.Remove(mEMBER);
                db.SaveChanges();
            }
            else
            {
                mEMBER.ActiveStatus = false;
                db.Entry(mEMBER).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
