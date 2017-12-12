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
    public class EventsController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Events
        public ActionResult Index(string sort, string order)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Display all events in a sorted list
            var eventList = db.EVENTs.ToList();

            bool asc = true;
            ViewBag.listSortAsc = "asc";
            if (order != null && order.Equals("asc"))
            {
                ViewBag.listSortAsc = "desc";
                asc = true;
            }
            else if(order != null && order.Equals("desc"))
            {
                ViewBag.listSortAsc = "asc";
                asc = false;
            }

            if (sort == null) return View(EventAssociationsViewModel.CreateEventAssociationsListFromModels(eventList, (int)memberId));

            // Events can be sorted by several model properties
            switch (sort)
            {
                case "title":
                    eventList = asc
                        ? eventList.OrderBy(e => e.EventTitle).ToList()
                        : eventList.OrderByDescending(e => e.EventTitle).ToList();
                    break;
                case "location":
                    eventList = asc
                        ? eventList.OrderBy(e => e.Location).ToList()
                        : eventList.OrderByDescending(e => e.Location).ToList();
                    break;
                case "date":
                    eventList = asc
                        ? eventList.OrderBy(e => e.EventDate).ToList()
                        : eventList.OrderByDescending(e => e.EventDate).ToList();
                    break;
            }

            return View(EventAssociationsViewModel.CreateEventAssociationsListFromModels(eventList, (int)memberId));
        }

        // GET: Events/Details/5
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

            // Find and display event details
            EVENT @event = db.EVENTs.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            // Create extended view model with basic associations
            EventAssociationsViewModel eventWithAssociations = EventAssociationsViewModel.CreateEventAssociationsFromModel(@event, (int)memberId);

            return View(eventWithAssociations);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage events
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Events");
            }

            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventTitle,Description,EventDate,Location,ActiveStatus,DateCreated")] EVENT @event)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage events
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Events");
            }

            // Validate and add event
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            db.EVENTs.Add(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage events
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Events");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find and display event for editing
            EVENT @event = db.EVENTs.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventTitle,Description,EventDate,Location,ActiveStatus,DateCreated")] EVENT @event)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage events
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Events");
            }

            // Validate and update event
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            db.Entry(@event).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = @event.EventId });
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage events
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Events");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find and display event details for deletion confirmation
            EVENT @event = db.EVENTs.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
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

            // Only admins and employees can manage events
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Events");
            }

            // Remove event and display list of events
            EVENT @event = db.EVENTs.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            db.EVENTs.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Events/Register/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(int id, string isRegistered)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Validate and update member/event registration
            if (!ModelState.IsValid) return RedirectToAction("Details", new {id = id});

            // Find the event
            EVENT @event = db.EVENTs.ToList().Find(x => x.EventId == id);

            // Cancelled events cannot be registered/unregistered while unactive
            if (@event == null)
            {
                return HttpNotFound();
            }
            else if (@event.ActiveStatus == false)
            {
                return RedirectToAction("Details", new { id = id });
            }

            // Remove registration if it exists
            if (isRegistered == "true")
            {
                MEMBER_EVENT memberEvent = db.MEMBER_EVENT.ToList().Find(x => x.EventId == id && x.MemberId == (int)memberId);

                if (memberEvent == null)
                {
                    return HttpNotFound();
                }

                db.MEMBER_EVENT.Remove(memberEvent);    
                db.SaveChanges();
                return RedirectToAction("Details",  new { id = id });
            }

            // Create new member/event registration
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

            return RedirectToAction("Details", new { id = id });
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
