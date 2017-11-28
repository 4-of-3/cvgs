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

            var aDDRESSes = db.ADDRESSes.Include(a => a.MEMBER).Include(a => a.PROVSTATE).Include(a => a.ADDRESSTYPE).ToList().FindAll(x => x.MemberId.Equals(memberId));
            return View(aDDRESSes.ToList());
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
            ADDRESS aDDRESS = db.ADDRESSes.Find(id);
            if (aDDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDDRESS);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,MemberId,StreetAddress,StreetAddress2,City,PostCode,ProvStateId,AddressTypeId")] ADDRESS aDDRESS)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (ModelState.IsValid)
            {
                db.ADDRESSes.Add(aDDRESS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode", aDDRESS.CountryId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", aDDRESS.MemberId);
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode", aDDRESS.ProvStateId);
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName", aDDRESS.AddressTypeId);
            return View(aDDRESS);
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
            ADDRESS aDDRESS = db.ADDRESSes.Find(id);
            if (aDDRESS == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode", aDDRESS.CountryId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", aDDRESS.MemberId);
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode", aDDRESS.ProvStateId);
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName", aDDRESS.AddressTypeId);
            return View(aDDRESS);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,MemberId,StreetAddress,StreetAddress2,City,PostCode,ProvStateId,AddressTypeId")] ADDRESS aDDRESS)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (ModelState.IsValid)
            {
                db.Entry(aDDRESS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CountryId = new SelectList(db.COUNTRies, "CountryId", "CountryCode", aDDRESS.CountryId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", aDDRESS.MemberId);
            ViewBag.ProvStateId = new SelectList(db.PROVSTATEs, "ProvStateId", "ProvStateCode", aDDRESS.ProvStateId);
            ViewBag.AddressTypeId = new SelectList(db.ADDRESSTYPEs, "AddressTypeId", "AddressTypeName", aDDRESS.AddressTypeId);
            return View(aDDRESS);
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
            ADDRESS aDDRESS = db.ADDRESSes.Find(id);
            if (aDDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDDRESS);
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

            ADDRESS aDDRESS = db.ADDRESSes.Find(id);
            db.ADDRESSes.Remove(aDDRESS);
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
