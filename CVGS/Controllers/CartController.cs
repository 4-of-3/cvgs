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
    public class CartController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Cart
        public ActionResult Index()
        {
            var cartItems = db.CARTITEMs.Include(c => c.GAME).Include(c => c.MEMBER);
            return View(cartItems.ToList());
        }

        // GET: Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARTITEM cartItem = db.CARTITEMs.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title");
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,GameId,Quantity,DateAdded")] CARTITEM cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CARTITEMs.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", cartItem.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", cartItem.MemberId);
            return View(cartItem);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARTITEM cARTITEM = db.CARTITEMs.Find(id);
            if (cARTITEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", cARTITEM.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", cARTITEM.MemberId);
            return View(cARTITEM);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,GameId,Quantity,DateAdded")] CARTITEM cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.GAMEs, "GameId", "Title", cartItem.GameId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", cartItem.MemberId);
            return View(cartItem);
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARTITEM cartItem = db.CARTITEMs.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CARTITEM cartItem = db.CARTITEMs.Find(id);
            db.CARTITEMs.Remove(cartItem);
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
