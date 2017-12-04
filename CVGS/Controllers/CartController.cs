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
    public class CartController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Cart
        public ActionResult IndexOld()
        {
            // Redirect unauthenticated members
            int? memberId = (int)Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
            
            var cartItems = db.CARTITEMs.Where(c => c.MemberId == memberId).Include(c => c.GAME).Include(c => c.MEMBER);
            return View(cartItems.ToList());
        }

        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            CartViewModel cart = new CartViewModel()
            {
                MemberId = (int)memberId,
                CartItems = db.CARTITEMs.Where(c => c.MemberId == (int)memberId).Include(c => c.GAME).Include(c => c.MEMBER).ToList()
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CartViewModel newCart)
        {
            CARTITEM cartItem;

            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                for (int i = 0; i < newCart.CartItems.Count; i++)
                {
                    var item = newCart.CartItems[i];
                    cartItem = db.CARTITEMs.ToList().Find(c => c.MemberId == item.MemberId && c.GameId == item.GameId);

                    cartItem.Quantity = item.Quantity;
                    db.Entry(cartItem).State = EntityState.Modified;
                    db.SaveChanges();
                }

                CartViewModel cart = new CartViewModel()
                {
                    MemberId = (int)memberId,
                    CartItems = db.CARTITEMs.Where(c => c.MemberId == (int)memberId).Include(c => c.GAME).Include(c => c.MEMBER).ToList()
                };
                return View(cart);
            }
            return View(newCart);
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
