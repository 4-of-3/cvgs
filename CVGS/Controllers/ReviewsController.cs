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

        // GET: Reviews/Pending
        public ActionResult Pending()
        {
            // Redirect unauthenticated members
            if (this.Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            var pendingList = db.REVIEWs.Include(x => x.MEMBER).Where(x => !x.Approved).OrderBy(x => x.DateCreated);

            return View(pendingList);
        }

        public ActionResult Approve(int? memberId, int? gameId)
        {
            // Redirect unauthenticated members
            if (this.Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            REVIEW review = db.REVIEWs.Find(memberId, gameId);

            if(review == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            review.Approved = true;
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Pending", "Reviews");
        }

        // GET: Reviews/Detials
        public ActionResult Details(int? memberId, int? gameId)
        {
            // Redirect unauthenticated members
            if (this.Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            REVIEW review = db.REVIEWs.Where(x => x.MemberId == memberId).ToList().Find(x => x.GameId == gameId);

            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(int? gameId)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            // Members who have already reviewed the game should be redirected to edit their review
            if (db.REVIEWs.Find(memberId, gameId) != null)
            {
                return RedirectToAction("Edit", new { memberId = memberId, gameId = gameId });
            }

            var review = new REVIEW()
            {
                GameId = (int)gameId,
                GAME = db.GAMEs.Find(gameId),
                MemberId = (int)memberId
            };

            return View(review);
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,GameId,ReviewText,Rating")] REVIEW review)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and add the review
            if (ModelState.IsValid)
            {
                review.DateCreated = DateTime.Now;
                db.REVIEWs.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }

            review.GAME = db.GAMEs.Find(review.GameId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? memberId, int? gameId)
        {
            // Redirect unauthenticated members
            if (this.Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (memberId != (int)Session["MemberId"])
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            // Find and display the review for editing
            REVIEW review = db.REVIEWs.Find(memberId, gameId);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,GameId,ReviewText,Rating,DateCreated")] REVIEW review)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and update the review
            if (!ModelState.IsValid) return View(review);

            review.DateModified = DateTime.Now;
            review.Approved = false;    //Review Needs to be approved after editing
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Games", new { id = review.GameId });
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? memberId, int? gameId)
        {
            // Redirect unauthenticated members
            if (this.Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Ensure only admins or the owner can delete a review
            if (memberId != (int)Session["MemberId"] && (string)Session["MemberRole"] != "Employee" && (string)Session["MemberRole"] != "Admin" )
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            // Find and display the review for deletion confirmation
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
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            //TODO: Implement Delete, probably with a view model

            // Remove review and display the game details with all reviews
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
