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
using System.Data.Entity.Core.Objects;

namespace CVGS.Controllers
{
    public class AccountController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: MEMBERs
        public ActionResult Index()
        {
            var memberId = this.Session["MemberId"];
            // Error catching for Address database call.
            try
            {
                var address = db.ADDRESSes.Where(r => r.MemberId == 4).ToList();
                ViewBag.address = address[0].StreetAddress;
            }
            catch (Exception)
            {
                ViewBag.address = "";
            }
            
            // Redirect unauthenticated members
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            MEMBER member = db.MEMBERs.Find(memberId);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: MEMBERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MEMBERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FName,LName,Email,UserName,Pwd,PwdConfirm,FavPlatform,FavCategory,FavGame,FavQuote")] NewAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SP_ADD_MEMBER(account.FName, account.LName, account.UserName, account.Email, account.Pwd, account.FavPlatform, account.FavCategory, account.FavGame, account.FavQuote);

                    var member = db.MEMBERs.Find(db.MEMBERs.Max(m => m.MemberId));
                    Session["MemberId"] = member.MemberId;
                    Session["MemberRole"] = member.ROLE.RoleName;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.GetBaseException());
                }
            }

            return View(account);
        }

        // GET: MEMBERs/Edit/5
        public ActionResult Edit(int? id)
        {
            //TODO: create ViewModel for editing account
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            
            EditAccountViewModel account = new EditAccountViewModel()
            {
                MemberId = member.MemberId,
                FName = member.FName,
                LName = member.LName,
                UserName = member.UserName,
                Email = member.Email,
                FavPlatform = member.FavPlatform,
                FavCategory = member.FavCategory,
                FavGame = member.FavGame,
                FavQuote = member.FavQuote
            };
            return View(account);
        }

        // POST: MEMBERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FName,LName,Email,FavPlatform,FavCategory,FavGame,FavQuote,Address")] EditAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                var member = db.MEMBERs.Find(account.MemberId);
                member.FName = account.FName;
                member.LName = account.LName;
                member.Email = account.Email;
                member.FavCategory = account.FavCategory;
                member.FavPlatform = account.FavPlatform;
                member.FavGame = account.FavGame;
                member.FavQuote = account.FavQuote;

                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: MEMBERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER member = db.MEMBERs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            DeleteAccountViewModel account = new DeleteAccountViewModel()
            {
                MemberId = member.MemberId,
                UserName = member.UserName,
                FullDelete = false
            };

            return View(account);
        }

        // POST: MEMBERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "MemberId,UserName,FullDelete")]DeleteAccountViewModel account)
        {
            int memberId = account.MemberId;
            MEMBER member = db.MEMBERs.Find(memberId);
            if (account.FullDelete)
            {
                db.MEMBERs.Remove(member);
                db.SaveChanges();
            }
            else
            {
                member.ActiveStatus = false;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();

            }
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        // GET: Address
        public ActionResult AddressIndex()
        {
            var memberId = this.Session["MemberId"];
            var address = db.ADDRESSes.Where(r => r.MemberId == (int)memberId).ToList();
            
            // Redirect unauthenticated members
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(address);
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
