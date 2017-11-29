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
    public class GamesController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Games
        public ActionResult Index(string search, string sort, string order)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            var gamesList = db.GAMEs.ToList();
            if(search != null)
            {
                gamesList = gamesList.FindAll(x => x.Title.ToLower().Contains(search.ToLower()));
                ViewBag.search = search;
            }

            bool asc = true;
            ViewBag.listSortAsc = "asc";
            if (order != null && order.Equals("asc"))
            {
                ViewBag.listSortAsc = "desc";
                asc = true;
            }
            else if (order != null && order.Equals("desc"))
            {
                ViewBag.listSortAsc = "asc";
                asc = false;
            }

            if (sort != null)
            {
                switch (sort)
                {
                    case "title":
                        gamesList = asc
                            ? gamesList.OrderBy(e => e.Title).ToList()
                            : gamesList.OrderByDescending(e => e.Title).ToList();
                        break;
                    case "category":
                        gamesList = asc
                            ? gamesList.OrderBy(e => e.Category).ToList()
                            : gamesList.OrderByDescending(e => e.Category).ToList();
                        break;
                    case "cost":
                        gamesList = asc
                            ? gamesList.OrderBy(e => e.Cost).ToList()
                            : gamesList.OrderByDescending(e => e.Cost).ToList();
                        break;
                    case "rating":
                        gamesList = asc
                            ? gamesList.OrderBy(e => (e.REVIEWs.Count()) == 0 ? 0 : e.REVIEWs.Average(m => m.Rating)).ToList()
                            : gamesList.OrderByDescending(e => (e.REVIEWs.Count()) == 0 ? 0 : e.REVIEWs.Average(m => m.Rating)).ToList();
                        break;
                }
            }

            return View(gamesList);
        }

        // GET: Games/Details/5
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

            GAME gAME = db.GAMEs.Find(id);
            ViewBag.gameTitle = gAME.Title;
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,Title,ISBN,Developer,Description,Category,PublicationDate,Cost,ImageUrl,Digital")] GAME gAME)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (!ModelState.IsValid)
            {
                return View(gAME);
            }

            db.GAMEs.Add(gAME);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Games/Edit/5
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

            GAME gAME = db.GAMEs.Find(id);
            ViewBag.gameTitle = gAME.Title;
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Title,ISBN,Developer,Description,Category,PublicationDate,Cost,ImageUrl,Digital")] GAME gAME)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            if (!ModelState.IsValid)
            {
                return View(gAME);
            }

            db.Entry(gAME).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Games/Delete/5
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

            GAME gAME = db.GAMEs.Find(id);
            ViewBag.gameTitle = gAME.Title;
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // POST: Games/Delete/5
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

            GAME gAME = db.GAMEs.Find(id);
            db.GAMEs.Remove(gAME);
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
