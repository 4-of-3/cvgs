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

            // Games can be filtered and ordered
            var gamesList = db.GAMEs.Where(g => !g.Deleted).ToList();

            // Only filter the list if a search term was provided
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

            if (sort == null) return View(GameAssociationsViewModel.CreateEventAssociationsListFromModels(gamesList, (int)memberId));

            // Handle list sorting
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

            return View(GameAssociationsViewModel.CreateEventAssociationsListFromModels(gamesList, (int)memberId));
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

            // Find and display game details
            GAME game = db.GAMEs.Where(g => !g.Deleted).ToList().Find(g=>g.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            // Create extended view model with basic associations
            GameAssociationsViewModel gameWithAssociations = GameAssociationsViewModel.CreateGameAssociationFromModel(game, (int) memberId);

            return View(gameWithAssociations);
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

            // Only admins and employees can manage games
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Games");
            }

            PLATFORM p = new PLATFORM();

            ViewBag.PlatformIdList = db.PLATFORMs;
            
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,ISBN,Developer,Description,Category,PublicationDate,Cost,ImageUrl,Digital,Platforms")] NewGameViewModel newGame)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage games
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Games");
            }

            if (newGame.Platforms == null)
            {
                ModelState.AddModelError("Platforms", "A game must be playable on at least one platform");
            }

            // Validate and add game
            if (!ModelState.IsValid)
            {
                return View(newGame);
            }

            GAME game = new GAME()
            {
                Title = newGame.Title,
                ISBN = newGame.ISBN,
                Developer = newGame.Developer,
                Description = newGame.Description,
                Category = newGame.Category,
                PublicationDate = newGame.PublicationDate,
                Cost = newGame.Cost,
                ImageUrl = newGame.ImageUrl,
                Digital = newGame.Digital
            };

            foreach (int platformId in newGame.Platforms)
            {
                game.PLATFORMs.Add(db.PLATFORMs.Find(platformId));
            }

            db.GAMEs.Add(game);
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

            // Only admins and employees can manage games
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Games");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find and display game for editing
            GAME game = db.GAMEs.Where(g => !g.Deleted).ToList().Find(g => g.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            ViewBag.gameTitle = game.Title;
            return View(game);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Title,ISBN,Developer,Description,Category,PublicationDate,Cost,ImageUrl,Digital")] GAME game)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can manage games
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Games");
            }

            // Validate and update game
            if (!ModelState.IsValid)
            {
                return View(game);
            }

            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = game.GameId });
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

            // Only admins and employees can manage games
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Games");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find and display game for deletion confirmation
            GAME game = db.GAMEs.Where(g => !g.Deleted).ToList().Find(g => g.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            ViewBag.gameTitle = game.Title;
            return View(game);
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

            // Only admins and employees can manage games
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to manage Games");
            }

            // Remove game and display all games
            GAME game = db.GAMEs.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            db.GAMEs.Remove(game);
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
