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

        // GET: Reviews
        public ActionResult Index()
        {
            var rEVIEWs = db.REVIEWs.Include(r => r.GAME).Include(r => r.MEMBER);
            return View(rEVIEWs.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? memberId, int? gameId)
        {
            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEWs.Find(memberId, gameId);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            return View(rEVIEW);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title");
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,GameId,ReviewText,Rating,DateCreated,DateModified")] REVIEW rEVIEW)
        {
            if (ModelState.IsValid)
            {
                db.REVIEWs.Add(rEVIEW);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", rEVIEW.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", rEVIEW.MemberId);
            return View(rEVIEW);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? memberId, int? gameId)
        {
            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEWs.Find(memberId, gameId);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", rEVIEW.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", rEVIEW.MemberId);
            return View(rEVIEW);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,GameId,ReviewText,Rating,DateCreated")] REVIEW rEVIEW)
        {
            if (ModelState.IsValid)
            {
                rEVIEW.DateModified = DateTime.Now;
                db.Entry(rEVIEW).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", rEVIEW.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", rEVIEW.MemberId);
            return View(rEVIEW);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? memberId, int? gameId)
        {
            if (memberId == null || gameId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEWs.Find(memberId, gameId);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            return View(rEVIEW);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REVIEW rEVIEW = db.REVIEWs.Find(id);
            db.REVIEWs.Remove(rEVIEW);
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
