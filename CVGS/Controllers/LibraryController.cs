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
    public class LibraryController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Library
        public ActionResult Index(string sort, string order)
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            var oRDERITEMs = db.ORDERITEMs.Include(o => o.GAME).Include(o => o.ORDERHEADER).ToList().FindAll(x => x.ORDERHEADER.MemberId.Equals(memberId) && x.ORDERHEADER.Processed);

            if (sort != null)
                oRDERITEMs = Sort(oRDERITEMs, sort, order);

            
            return View(oRDERITEMs);
        }

        public ActionResult Download()
        {
            return View();
        }

        private List<ORDERITEM> Sort(List<ORDERITEM> list, string sort, string order)
        {
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

            // Handle list sorting
            switch (sort)
            {
                case "title":
                    list = asc
                        ? list.OrderBy(e => e.GAME.Title).ToList()
                        : list.OrderByDescending(e => e.GAME.Title).ToList();
                    break;
                case "category":
                    list = asc
                        ? list.OrderBy(e => e.GAME.Category).ToList()
                        : list.OrderByDescending(e => e.GAME.Category).ToList();
                    break;
            }

            return list;
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
