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

            // Display all the member's credit cards
            var creditCards = db.CREDITCARDs.Include(c => c.MEMBER).Where(c => !c.Deleted).ToList().FindAll(x=>x.MemberId.Equals(memberId));
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

            // Find and display the credit card details
            CREDITCARD creditCard = db.CREDITCARDs.Where(c => !c.Deleted).ToList().Find(c=>c.CardId == id);
            if (creditCard == null || !creditCard.MemberId.Equals(memberId))
            {
                return HttpNotFound();
            }
            return View(creditCard);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardId,MemberId,CardNumber,CardDescription,NameOnCard,ExpiryDate,CVV")] CREDITCARD creditCard)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and add credit card
            if (ModelState.IsValid)
            {
                db.CREDITCARDs.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Prepare form when validation errors are found
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", creditCard.MemberId);
            return View(creditCard);
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

            // Find and display credit card for editing
            CREDITCARD creditCard = db.CREDITCARDs.Where(c => !c.Deleted).ToList().Find(c => c.CardId == id);
            if (creditCard == null || !creditCard.MemberId.Equals(memberId))
            {
                return HttpNotFound();
            }

            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", creditCard.MemberId);
            return View(creditCard);
        }

        // POST: CreditCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardId,MemberId,CardNumber,CardDescription,NameOnCard,ExpiryDate,CVV")] CREDITCARD creditCard)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and update the credit card
            if (ModelState.IsValid)
            {
                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = creditCard.CardId });
            }

            // Prepare the form when validation errors are found
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", creditCard.MemberId);
            return View(creditCard);
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

            // Find and display the credit card for deletion confirmation
            CREDITCARD creditCard = db.CREDITCARDs.Where(c => !c.Deleted).ToList().Find(c => c.CardId == id);
            if (creditCard == null || !creditCard.MemberId.Equals(memberId))
            {
                return HttpNotFound();
            }

            return View(creditCard);
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

            // Remove credit card and display list of cards
            CREDITCARD creditCard = db.CREDITCARDs.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }

            db.CREDITCARDs.Remove(creditCard);
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
