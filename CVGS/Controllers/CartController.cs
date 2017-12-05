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
                // Update all items in the cart
                for (int i = 0; i < newCart.CartItems.Count; i++)
                {
                    var item = newCart.CartItems[i];
                    cartItem = db.CARTITEMs.Find(item.MemberId, item.GameId);

                    cartItem.Quantity = item.Quantity;

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

                CartViewModel cart = new CartViewModel()
                {
                    MemberId = (int)memberId,
                    CartItems = db.CARTITEMs.Where(c => c.MemberId == (int)memberId).Include(c => c.GAME).Include(c => c.MEMBER).ToList()
                };
                return View(cart);
            }

            for (int i = 0; i < newCart.CartItems.Count; i++)
            {
                newCart.CartItems[i].GAME = db.GAMEs.Find(newCart.CartItems[i].GameId);
            }

            return View(newCart);
        }

        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            CARTITEM cartItem = new CARTITEM()
            {
                MemberId = (int)memberId,
                GameId = (int)id,
                Quantity = 1
            };

            db.CARTITEMs.Add(cartItem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            try
            {
                CARTITEM cartItem = db.CARTITEMs.Find((int)memberId, (int)id);
                db.CARTITEMs.Remove(cartItem);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return HttpNotFound();
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
