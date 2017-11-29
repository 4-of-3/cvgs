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
    public class FriendsController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Friends
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            int member = (int)memberId;

            List<FRIENDSHIP> friends;
            friends = db.FRIENDSHIPs.Where(f => f.MemberId == member).ToList();
            return View(friends);
        }

        // GET: Friends/Details/5
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
            FRIENDSHIP fRIENDSHIP = db.FRIENDSHIPs.Find(id);
            if (fRIENDSHIP == null)
            {
                return HttpNotFound();
            }
            return View(fRIENDSHIP);
        }

        // GET: Friends/Create
        public ActionResult Create()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            ViewBag.FriendId = new SelectList(db.MEMBERs, "MemberId", "FName");
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName");
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DateCreated,MemberId,FriendId")] FRIENDSHIP fRIENDSHIP)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (ModelState.IsValid)
            {
                db.FRIENDSHIPs.Add(fRIENDSHIP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FriendId = new SelectList(db.MEMBERs, "MemberId", "FName", fRIENDSHIP.FriendId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", fRIENDSHIP.MemberId);
            return View(fRIENDSHIP);
        }

        // GET: Friends/Edit/5
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
            FRIENDSHIP fRIENDSHIP = db.FRIENDSHIPs.Find(id);
            if (fRIENDSHIP == null)
            {
                return HttpNotFound();
            }
            ViewBag.FriendId = new SelectList(db.MEMBERs, "MemberId", "FName", fRIENDSHIP.FriendId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", fRIENDSHIP.MemberId);
            return View(fRIENDSHIP);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DateCreated,MemberId,FriendId")] FRIENDSHIP fRIENDSHIP)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (ModelState.IsValid)
            {
                db.Entry(fRIENDSHIP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FriendId = new SelectList(db.MEMBERs, "MemberId", "FName", fRIENDSHIP.FriendId);
            ViewBag.MemberId = new SelectList(db.MEMBERs, "MemberId", "FName", fRIENDSHIP.MemberId);
            return View(fRIENDSHIP);
        }

        // GET: Friends/Delete/5
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
            FRIENDSHIP fRIENDSHIP = db.FRIENDSHIPs.Find(id);
            if (fRIENDSHIP == null)
            {
                return HttpNotFound();
            }
            return View(fRIENDSHIP);
        }

        // POST: Friends/Add/3
        public ActionResult AddFriend(int id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }
            
            if (ModelState.IsValid)
            {
                FRIENDSHIP friendship = new FRIENDSHIP();
                friendship.FriendId = id;
                friendship.MemberId = (int)memberId;
                try
                {
                    db.FRIENDSHIPs.Add(friendship);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return HttpNotFound();
                }
                
                return RedirectToAction("Index", "Friends");
            }
            return RedirectToAction("Index", "Friends");
        }

        // POST: Friends/Remove/3
        public ActionResult RemoveFriend(int? id)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            try
            {
                FRIENDSHIP friendship = db.FRIENDSHIPs.Find(memberId, id);
                db.FRIENDSHIPs.Remove(friendship);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            
            return RedirectToAction("Index", "Members");
        }

        // GET: Friends/Edit/5
        public ActionResult SearchFriends(string username)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            return RedirectToAction("Index", "Members");
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
