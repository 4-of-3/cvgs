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
            var oRDERHEADERs = db.ORDERHEADERs.Include(o => o.ADDRESS).Include(o => o.ADDRESS1).Include(o => o.CREDITCARD).Include(o => o.MEMBER);
            return View(oRDERHEADERs.ToList());
        }

        public ActionResult InProcess()
        {
            var oRDERHEADERs = db.ORDERHEADERs.Include(o => o.ADDRESS).Include(o => o.ADDRESS1).Include(o => o.CREDITCARD).Include(o => o.MEMBER);
            return View(oRDERHEADERs.ToList());
        }

        public ActionResult Process(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERHEADER oRDERHEADER = db.ORDERHEADERs.Find(id);
            if (oRDERHEADER == null)
            {
                return HttpNotFound();
            }
            return View(oRDERHEADER);
        }

        // POST: Order/Process/5
        [HttpPost, ActionName("Process")]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessConfirmed(int id)
        {
            ORDERHEADER oRDERHEADER = db.ORDERHEADERs.Find(id);
            
            db.ORDERHEADERs.Remove(oRDERHEADER);
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
            ORDERHEADER oRDERHEADER = db.ORDERHEADERs.Find(id);
            if (oRDERHEADER == null)
            {
                return HttpNotFound();
            }
            return View(oRDERHEADER);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.BillingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress");
            ViewBag.ShippingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress");
            ViewBag.CreditCardId = new SelectList(db.CREDITCARDs, "CardId", "CardNumber");
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,MemberId,BillingAddressId,ShippingAddressId,CreditCardId,DateCreated")] ORDERHEADER oRDERHEADER)
        {
            if (ModelState.IsValid)
            {
                db.ORDERHEADERs.Add(oRDERHEADER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", oRDERHEADER.BillingAddressId);
            ViewBag.ShippingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", oRDERHEADER.ShippingAddressId);
            ViewBag.CreditCardId = new SelectList(db.CREDITCARDs, "CardId", "CardNumber", oRDERHEADER.CreditCardId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", oRDERHEADER.MemberId);
            return View(oRDERHEADER);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERHEADER oRDERHEADER = db.ORDERHEADERs.Find(id);
            if (oRDERHEADER == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", oRDERHEADER.BillingAddressId);
            ViewBag.ShippingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", oRDERHEADER.ShippingAddressId);
            ViewBag.CreditCardId = new SelectList(db.CREDITCARDs, "CardId", "CardNumber", oRDERHEADER.CreditCardId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", oRDERHEADER.MemberId);
            return View(oRDERHEADER);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,MemberId,BillingAddressId,ShippingAddressId,CreditCardId,DateCreated")] ORDERHEADER oRDERHEADER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDERHEADER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", oRDERHEADER.BillingAddressId);
            ViewBag.ShippingAddressId = new SelectList(db.ADDRESSes, "AddressId", "StreetAddress", oRDERHEADER.ShippingAddressId);
            ViewBag.CreditCardId = new SelectList(db.CREDITCARDs, "CardId", "CardNumber", oRDERHEADER.CreditCardId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", oRDERHEADER.MemberId);
            return View(oRDERHEADER);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERHEADER oRDERHEADER = db.ORDERHEADERs.Find(id);
            if (oRDERHEADER == null)
            {
                return HttpNotFound();
            }
            return View(oRDERHEADER);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDERHEADER oRDERHEADER = db.ORDERHEADERs.Find(id);
            db.ORDERHEADERs.Remove(oRDERHEADER);
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
