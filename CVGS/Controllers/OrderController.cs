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
    public class OrderController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Order
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var memberRole = (string)Session["MemberRole"];
            if (memberRole != "Employee" && memberRole != "Admin")
            {
                return new HttpUnauthorizedResult("You are not authorized to see all orders");
            }

            var oRDERHEADERs = db.ORDERHEADERs.Include(o => o.ADDRESS).Include(o => o.ADDRESS1).Include(o => o.CREDITCARD).Include(o => o.MEMBER);
            return View(oRDERHEADERs.ToList());
        }

        public ActionResult MyOrders()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var oRDERHEADERs = db.ORDERHEADERs.Where(o=>o.MemberId == (int)memberId).Include(o => o.ADDRESS).Include(o => o.ADDRESS1).Include(o => o.CREDITCARD).Include(o => o.MEMBER);
            return View(oRDERHEADERs.ToList());
        }

        public ActionResult Checkout()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var memberAddresses = db.ADDRESSes.Where(a => a.MemberId == (int)memberId && !a.Deleted);
            var memberCreditCards = db.CREDITCARDs.Where(c => c.MemberId == (int)memberId && !c.Deleted);
            int shippingAddressIndex = 0;
            int billingAddressIndex = 0;
            // If no address exists for the user, prompt them to add an address
            if (memberAddresses.Count() <1)
            {
                return RedirectToAction("NoAddress");
            }
            // If no credit card has been added for the user, prompt them to add a credit card
            if (memberCreditCards.Count() < 1)
            {
                return RedirectToAction("NoCreditCard");
            }

            ORDERHEADER order = new ORDERHEADER()
            {
                MemberId = (int)memberId,
                DateCreated = DateTime.Now,
                Processed = false
            };
            var cartItems = db.CARTITEMs.Where(c => c.MemberId == (int)memberId).Include(c=>c.GAME);
            foreach (var cartItem in cartItems)
            {
                ORDERITEM orderItem = new ORDERITEM()
                {
                    OrderId = order.OrderId,
                    GameId = cartItem.GameId,
                    Quantity = cartItem.Quantity,
                    GAME = cartItem.GAME
                };
                order.ORDERITEMs.Add(orderItem);
            }

            if (memberAddresses.Where(a=>a.ADDRESSTYPE.AddressTypeName == "Billing").Count() > 0)
            {
                billingAddressIndex = memberAddresses.Where(a => a.ADDRESSTYPE.AddressTypeName == "Billing").FirstOrDefault().AddressId;
            }
            if (memberAddresses.Where(a => a.ADDRESSTYPE.AddressTypeName == "Shipping").Count() > 0)
            {
                shippingAddressIndex = memberAddresses.Where(a => a.ADDRESSTYPE.AddressTypeName == "Shipping").FirstOrDefault().AddressId;
            }
            
            ViewBag.BillingAddressId = new SelectList(memberAddresses, "AddressId", "StreetAddress", billingAddressIndex);
            ViewBag.ShippingAddressId = new SelectList(memberAddresses, "AddressId", "StreetAddress", shippingAddressIndex);
            ViewBag.CreditCardId = new SelectList(memberCreditCards, "CardId", "CardNumber");

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "OrderId,MemberId,BillingAddressId,ShippingAddressId,CreditCardId,DateCreated")] ORDERHEADER orderHeader)
        {
            if (ModelState.IsValid)
            {
                db.ORDERHEADERs.Add(orderHeader);
                var cartItems = db.CARTITEMs.Where(c => c.MemberId == orderHeader.MemberId).Include(c => c.GAME);
                foreach (var cartItem in cartItems)
                {
                    ORDERITEM orderItem = new ORDERITEM()
                    {
                        OrderId = orderHeader.OrderId,
                        GameId = cartItem.GameId,
                        Quantity = cartItem.Quantity
                    };
                    db.ORDERITEMs.Add(orderItem);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", orderHeader.BillingAddressId);
            ViewBag.ShippingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", orderHeader.ShippingAddressId);
            ViewBag.CreditCardId = new SelectList(db.CREDITCARDs, "CardId", "CardNumber", orderHeader.CreditCardId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", orderHeader.MemberId);
            return View(orderHeader);
        }

        public ActionResult InProcess()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string memberRole = (string)Session["MemberRole"];
            if(memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to process Orders");
            }

            var orderHeaders = db.ORDERHEADERs.Where(o => !o.Processed).Include(o => o.ADDRESS).Include(o => o.ADDRESS1).Include(o => o.CREDITCARD).Include(o => o.MEMBER);
            return View(orderHeaders.ToList());
        }

        public ActionResult Process(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERHEADER orderHeader = db.ORDERHEADERs.Find(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }

        // POST: Order/Process/5
        [HttpPost, ActionName("Process")]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessConfirmed(int id)
        {
            ORDERHEADER orderHeader = db.ORDERHEADERs.Find(id);
            orderHeader.Processed = true;
            db.Entry(orderHeader).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERHEADER orderHeader = db.ORDERHEADERs.Find(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }
        
        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERHEADER orderHeader = db.ORDERHEADERs.Find(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDERHEADER orderHeader = db.ORDERHEADERs.Find(id);
            db.ORDERHEADERs.Remove(orderHeader);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NoAddress()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult NoCreditCard()
        {
            // Redirect unauthenticated members
            var memberId = Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
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
