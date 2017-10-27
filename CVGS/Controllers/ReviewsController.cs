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
    public class ReviewsController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Reviews/Create
        public ActionResult Create(int? gameId)
        {
            if (gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int memberId = (int)Session["MemberId"];

            if (db.REVIEWs.Find(memberId, gameId) != null)
            {
                // Member has already reviewed this game; redirect to edit
                return RedirectToAction("Edit", new { memberId = memberId, gameId = gameId });
            }

            var review = new REVIEW()
            {
                GameId = (int)gameId,
                GAME = db.GAMEs.Find(gameId),
                MemberId = (int)Session["MemberId"],
                MEMBER = db.MEMBERs.Find((int)Session["MemberId"])
            };

            return View(review);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,GameId,ReviewText,Rating")] REVIEW review)
        {
            if (ModelState.IsValid)
            {
                review.DateCreated = DateTime.Now;
                db.REVIEWs.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }

            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", review.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", review.MemberId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? memberId, int? gameId)
        {
            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (memberId != (int)Session["MemberId"])
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            REVIEW review = db.REVIEWs.Find(memberId, gameId);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,GameId,ReviewText,Rating,DateCreated")] REVIEW review)
        {
            if (ModelState.IsValid)
            {
                review.DateModified = DateTime.Now;
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Games", new { id=review.GameId });
            }
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", review.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", review.MemberId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? memberId, int? gameId)
        {
            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (memberId != (int)Session["MemberId"] && (string)Session["MemberRole"] != "Admin" && (string)Session["MemberRole"] != "Employee" )
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            REVIEW review = db.REVIEWs.Find(memberId, gameId);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "MemberId,GameId")] REVIEW reviewToDelete)
        {
            //TODO Implement Delete, probably with a view model
            REVIEW review = db.REVIEWs.Find(reviewToDelete.MemberId, reviewToDelete.GameId);
            db.REVIEWs.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Details", "Games", new { id = reviewToDelete.GameId });
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
