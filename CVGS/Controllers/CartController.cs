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

        public ActionResult Index(int redirectGameId = -1)
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

            // Enable user to return to a game after viewing the cart (can be from add/update)
            if (redirectGameId >= 0)
            {
                ViewBag.ReturnGameId = redirectGameId;
            }

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CartViewModel newCart)
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            // Validate model and display errors
            if (ModelState.IsValid)
            {
                // Update all items in the cart
                foreach (var item in newCart.CartItems)
                {
                    CARTITEM cartItem = db.CARTITEMs.Find(item.MemberId, item.GameId);

                    // Handle invalid cart items (simply skip, don't throw an exception)
                    if (cartItem == null)
                    {
                        continue;
                    }

                    cartItem.Quantity = item.Quantity;

                    // Remove cart items with a quantity of zero
                    if (cartItem.Quantity == 0)
                    {
                        db.CARTITEMs.Remove(cartItem);
                    }
                    else
                    {
                        db.Entry(cartItem).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            // Prepare items for error display
            foreach (CARTITEM item in newCart.CartItems)
            {
                item.GAME = db.GAMEs.Find(item.GameId);
            }

            return View("Index", newCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CartViewModel newCart)
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Validate model and display errors
            if (ModelState.IsValid)
            {
                // Update all items in the cart
                foreach (var item in newCart.CartItems)
                {
                    CARTITEM cartItem = db.CARTITEMs.Find(item.MemberId, item.GameId);

                    // Handle invalid cart items (simply skip, don't throw an exception)
                    if (cartItem == null)
                    {
                        continue;
                    }

                    cartItem.Quantity = item.Quantity;

                    // Remove cart items with a quantity of zero
                    if (cartItem.Quantity == 0)
                    {
                        db.CARTITEMs.Remove(cartItem);
                    }
                    else
                    {
                        db.Entry(cartItem).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Checkout", "Order");
            }

            try
            {
                // Prepare items for error display
                foreach (CARTITEM item in newCart.CartItems)
                {
                    item.GAME = db.GAMEs.Find(item.GameId);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View("Index", newCart);
        }

        public ActionResult Add(int? id)
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Create and add new cart item
            CARTITEM cartItem = new CARTITEM()
            {
                MemberId = (int)memberId,
                GameId = (int)id,
                Quantity = 1
            };

            // Enable users to return to Game Details page after adding a game to the cart
            int redirectGameId = (int)id;

            db.CARTITEMs.Add(cartItem);
            db.SaveChanges();

            return RedirectToAction("Index", new { redirectGameId });
        }

        public ActionResult Delete(int? id)
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CARTITEM cartItem = db.CARTITEMs.Find((int)memberId, (int)id);

            // Handle invalid cart item removals
            if (cartItem == null)
            {
                return new HttpNotFoundResult();
            }

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
