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
    public class WishlistController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Wishlist
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Select member's wishlist items
            List<WISHLISTITEM> wishlistItems = db.WISHLISTITEMs.Include(w => w.GAME).Where(w => w.MemberId == (int)memberId).ToList();

            List<WishlistItemViewModel> wishlistItemAssociations = WishlistItemViewModel.CreateWishlistItemAssociationsListFromModels(wishlistItems);

            return View(wishlistItemAssociations);
        }

        // GET: Wishlist/Toggle
        public ActionResult Toggle(int? id)
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

            // Check if the game has already been added to the member's wishlist (remove in this case)
            WISHLISTITEM wishlistItem = db.WISHLISTITEMs.ToList().Find(w => w.MemberId == (int)memberId && w.GameId == (int)id);

            // Create and add wish list item. If a game is alread in the wishlist it should be removed
            if (wishlistItem == null)
            {
                // Create and add wish list item
                wishlistItem = new WISHLISTITEM()
                {
                    MemberId = (int)memberId,
                    GameId = (int)id,
                    DateAdded = DateTime.Now
                };

                db.WISHLISTITEMs.Add(wishlistItem);
            }
            else
            {
                db.WISHLISTITEMs.Remove(wishlistItem);
            }

            db.SaveChanges();

            return RedirectToAction("Details", "Games", new { id });
        }

        // GET: Wishlist/Remove/5
        public ActionResult Remove(int? id)
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

            WISHLISTITEM wishlistItem = db.WISHLISTITEMs.Find((int)memberId, id);

            // Handle invalid cart item removals
            if (wishlistItem == null)
            {
                return HttpNotFound();
            }

            // Remove wishlist item
            db.WISHLISTITEMs.Remove(wishlistItem);
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
