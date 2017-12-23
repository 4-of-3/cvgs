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

            // Convert list of games to games with associations
            List<GameAssociationsViewModel> gameAssociations = GameAssociationsViewModel.CreateGameAssociationsListFromModels(gamesList, (int)memberId);

            if (sort == null) return View(gameAssociations);

            // Handle list sorting
            switch (sort)
            {
                case "title":
                    gameAssociations = asc
                        ? gameAssociations.OrderBy(e => e.Title).ToList()
                        : gameAssociations.OrderByDescending(e => e.Title).ToList();
                    break;
                case "category":
                    gameAssociations = asc
                        ? gameAssociations.OrderBy(e => e.Category).ToList()
                        : gameAssociations.OrderByDescending(e => e.Category).ToList();
                    break;
                case "cost":
                    gameAssociations = asc
                        ? gameAssociations.OrderBy(e => e.Cost).ToList()
                        : gameAssociations.OrderByDescending(e => e.Cost).ToList();
                    break;
                case "rating":
                    gameAssociations = asc
                        ? gameAssociations.OrderBy(e => (e.ApprovedReviews.Count()) == 0 ? 0 : e.ApprovedReviews.Average(m => m.Rating)).ToList()
                        : gameAssociations.OrderByDescending(e => (e.ApprovedReviews.Count()) == 0 ? 0 : e.ApprovedReviews.Average(m => m.Rating)).ToList();
                    break;
            }

            return View(gameAssociations);
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
        public ActionResult Create([Bind(Include = "Title,ISBN,Developer,Description,Category,PublicationDate,Cost,ImageUrl,Digital,Platforms")] AddEditGameViewModel newGame)
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

            // Validate and add game
            if (!ModelState.IsValid)
            {
                ViewBag.PlatformIdList = db.PLATFORMs;
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

            // Games can have multiple platforms associated with them
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

            AddEditGameViewModel newGame = new AddEditGameViewModel()
            {
                GameId = game.GameId,
                Title = game.Title,
                ISBN = game.ISBN,
                Developer = game.Developer,
                Description = game.Description,
                Category = game.Category,
                PublicationDate = game.PublicationDate,
                Cost = game.Cost,
                ImageUrl = game.ImageUrl,
                Digital = game.Digital,
                Platforms = game.PLATFORMs.Select(p => p.PlatformId).ToList()
            };

            if (game == null)
            {
                return HttpNotFound();
            }

            ViewBag.gameTitle = game.Title;
            ViewBag.PlatformIdList = db.PLATFORMs;

            return View(newGame);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Title,ISBN,Developer,Description,Category,PublicationDate,Cost,ImageUrl,Digital,Platforms")] AddEditGameViewModel newGame)
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
                ViewBag.gameTitle = newGame.Title;
                ViewBag.PlatformIdList = db.PLATFORMs;
                return View(newGame);
            }

            GAME game = db.GAMEs.Find(newGame.GameId);
            game.Title = newGame.Title;
            game.ISBN = newGame.ISBN;
            game.Developer = newGame.Developer;
            game.Description = newGame.Description;
            game.Category = newGame.Category;
            game.PublicationDate = newGame.PublicationDate;
            game.Cost = newGame.Cost;
            game.ImageUrl = newGame.ImageUrl;
            game.Digital = newGame.Digital;

            // Remove platforms that are not in the new game model
            for (int i = game.PLATFORMs.Count - 1; i >= 0; i--)
            {
                var platform = game.PLATFORMs.ToList()[i];
                if (!newGame.Platforms.Contains(platform.PlatformId))
                {
                    game.PLATFORMs.Remove(platform);
                }
            }

            // Add platforms that are not in the old game model
            foreach (int platformId in newGame.Platforms)
            {
                if (!game.PLATFORMs.Select(p=>p.PlatformId).Contains(platformId))
                {
                    game.PLATFORMs.Add(db.PLATFORMs.Find(platformId));
                }
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
