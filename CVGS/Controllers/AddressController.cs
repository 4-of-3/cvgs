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
    public class AddressController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Address
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Display all the member's addresses
            var addresses = db.ADDRESSes.Include(a => a.MEMBER).Include(a => a.PROVSTATE).Include(a => a.ADDRESSTYPE).Where(a=>!(bool)a.Deleted).ToList().FindAll(x => x.MemberId.Equals(memberId));
            return View(addresses.ToList());
        }

        // GET: Address/Details/5
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

            // Find and display the address details
            ADDRESS address = db.ADDRESSes.Where(a => !(bool)a.Deleted).ToList().Find(a => a.AddressId == id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode");
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName");
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode");
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName");
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,MemberId,StreetAddress,StreetAddress2,City,PostCode,ProvStateId,AddressTypeId")] ADDRESS address)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and add the address
            if (ModelState.IsValid)
            {
                db.ADDRESSes.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Prepare form when validation errors are found
            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode", address.CountryId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", address.MemberId);
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode", address.ProvStateId);
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName", address.AddressTypeId);
            return View(address);
        }

        // GET: Address/Edit/5
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

            // Find and display the address for editing
            ADDRESS address = db.ADDRESSes.Where(a => !(bool)a.Deleted).ToList().Find(a => a.AddressId == id);
            if (address == null)
            {
                return HttpNotFound();
            }

            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode", address.CountryId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", address.MemberId);
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode", address.ProvStateId);
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName", address.AddressTypeId);
            return View(address);
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,MemberId,StreetAddress,StreetAddress2,City,PostCode,ProvStateId,AddressTypeId")] ADDRESS address)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and update the address
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = address.AddressId});
            }

            // Prepare form when validation errors are found
            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode", address.CountryId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", address.MemberId);
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode", address.ProvStateId);
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName", address.AddressTypeId);
            return View(address);
        }

        // GET: Address/Delete/5
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

            // Find and display address for deletion confirmation
            ADDRESS address = db.ADDRESSes.Where(a => !(bool)a.Deleted).ToList().Find(a => a.AddressId == id);
            if (address == null)
            {
                return HttpNotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
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

            // Remove address and display address list
            ADDRESS address = db.ADDRESSes.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            db.ADDRESSes.Remove(address);
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
