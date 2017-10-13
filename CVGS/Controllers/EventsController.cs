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
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
            return View(db.EVENTs.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool registered = isRegistered((int)id, (int)memberId);
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.isRegistered = registered; 
            return View(eVENT);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventTitle,Description,EventDate,Location,ActiveStatus,DateCreated")] EVENT eVENT)
        {
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
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
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
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
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
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
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
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
        public ActionResult Details(int id, string isRegistered)
        {
            var memberId = this.Session["memberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
            if (ModelState.IsValid)
            {
                if (isRegistered == "true")
                {
                    MEMBER_EVENT memberEvent = db.MEMBER_EVENT.ToList().Find(x => x.EventId == id && x.MemberId == (int)memberId);
                    db.MEMBER_EVENT.Remove(memberEvent);    
                    db.SaveChanges();
                    return RedirectToAction("Details",  new { id = id });
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
                    catch (Exception ex)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // sad path
                    }
                }
            }
            return RedirectToAction("Details", new { id = id });
        }

        private bool isRegistered(int eventId, int memberId)
        {
            MEMBER_EVENT memberEvent = new MEMBER_EVENT();
            try
            {
                memberEvent = db.MEMBER_EVENT.ToList().Find(x => x.EventId == eventId && x.MemberId == memberId);
            }
            catch (Exception)
            {
                return false;
            }
            if (memberEvent == null)
            {
                return false;
            }
            return true;
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
