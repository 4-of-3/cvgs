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
    public class EventsController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Events
        public ActionResult Index()
        {
            var memberId = this.Session["MemberId"];
            return View(db.EVENTs.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventTitle,Description,EventDate,Location,ActiveStatus,DateCreated")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.EVENTs.Add(eVENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eVENT);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventTitle,Description,EventDate,Location,ActiveStatus,DateCreated")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eVENT);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENT eVENT = db.EVENTs.Find(id);
            db.EVENTs.Remove(eVENT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Events/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                var memberId = this.Session["memberId"];
                if (memberId == null)
                {
                    return RedirectToAction("Index", "Login"); ;
                }
                MEMBER_EVENT memberRegister = new MEMBER_EVENT();
                memberRegister.EventId = id;
                memberRegister.MemberId = (int)this.Session["MemberId"];
                memberRegister.DateRegistered = System.DateTime.Now;
                if (memberRegister.ToString() != "")
                {
                    try
                    {
                        db.MEMBER_EVENT.Add(memberRegister);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // sad path
                    }
                }
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
