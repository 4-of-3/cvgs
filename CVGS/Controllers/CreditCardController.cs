﻿using System;
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
    public class CreditCardController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: CreditCard
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            var creditCards = db.CREDITCARDs.Include(c => c.MEMBER).ToList().FindAll(x=>x.MemberId.Equals(memberId));
            return View(creditCards);
        }

        // GET: CreditCard/Details/5
        public ActionResult Details(int? id)
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
            CREDITCARD cREDITCARD = db.CREDITCARDs.Find(id);
            if (cREDITCARD == null || !cREDITCARD.MemberId.Equals(memberId))
            {
                return HttpNotFound();
            }
            return View(cREDITCARD);
        }

        // GET: CreditCard/Create
        public ActionResult Create()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            ViewBag.MemberId = memberId;
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardId,MemberId,CardNumber,NameOnCard,ExpiryDate")] CREDITCARD cREDITCARD)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (ModelState.IsValid)
            {
                db.CREDITCARDs.Add(cREDITCARD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", cREDITCARD.MemberId);
            return View(cREDITCARD);
        }

        // GET: CreditCard/Edit/5
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
            CREDITCARD cREDITCARD = db.CREDITCARDs.Find(id);
            if (cREDITCARD == null || !cREDITCARD.MemberId.Equals(memberId))
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", cREDITCARD.MemberId);
            return View(cREDITCARD);
        }

        // POST: CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardId,MemberId,CardNumber,NameOnCard,ExpiryDate")] CREDITCARD cREDITCARD)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (ModelState.IsValid)
            {
                db.Entry(cREDITCARD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", cREDITCARD.MemberId);
            return View(cREDITCARD);
        }

        // GET: CreditCard/Delete/5
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
            CREDITCARD cREDITCARD = db.CREDITCARDs.Find(id);
            if (cREDITCARD == null || !cREDITCARD.MemberId.Equals(memberId))
            {
                return HttpNotFound();
            }
            return View(cREDITCARD);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            CREDITCARD cREDITCARD = db.CREDITCARDs.Find(id);
            db.CREDITCARDs.Remove(cREDITCARD);
            db.SaveChanges();
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
