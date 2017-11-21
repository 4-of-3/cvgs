using CVGS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVGS.Tests.Models
{
    [TestClass]
    public class ReviewModelTest
    {
        CVGSEntities db = new CVGSEntities();
        GAME game;
        MEMBER member;
        REVIEW review;
        string reviewText = "Test review text";

        [TestInitialize]
        public void ReviewTestSetUp()
        {
            game = db.GAMEs.ToList().LastOrDefault();
        }

        [TestMethod]
        public void AddReview()
        {
            //find a member who has not added a review to the selected game
            member = db.MEMBERs.Find((from m in db.MEMBERs
                                      where !(from r in db.REVIEWs
                                              where r.GameId == game.GameId
                                              select r.MemberId).Contains(m.MemberId)
                                      select m.MemberId).FirstOrDefault());

            if(member == null)
            {
                // If all members have reviewed the game,
                // create a new game to be reviewed
                game = new GAME()
                {
                    Title = "A Game Title",
                    ISBN = "0123456789",
                    Developer = "Developer",
                    Description = "Description",
                    Category = "Category",
                    Cost = 15.5M,
                    Digital = true,
                    Deleted = false
                };
                db.GAMEs.Add(game);
                db.SaveChanges();
            }
            
            review = new REVIEW()
            {
                GameId = game.GameId,
                MemberId = member.MemberId,
                Rating = 3,
                ReviewText = reviewText,
                DateCreated = DateTime.Now
            };

            db.REVIEWs.Add(review);
            db.SaveChanges();

            review = db.REVIEWs.Find(review.MemberId, review.GameId);
            Assert.AreEqual(review.ReviewText, reviewText);
        }

        [TestMethod]
        public void EditReview()
        {
            review = db.REVIEWs.Where(r=>r.GameId == game.GameId 
                                   && r.ReviewText == reviewText
                                   && r.DateCreated == db.REVIEWs.Max(i => i.DateCreated))
                                   .Single();

            review.DateModified = DateTime.Now;
            db.Entry(review).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            review = db.REVIEWs.Find(review.MemberId, review.GameId);

            Assert.IsNotNull(review.DateModified);
        }

        [TestMethod]
        public void DeleteReview()
        {
            review = db.REVIEWs.Where(r => r.GameId == game.GameId
                                   && r.ReviewText == reviewText
                                   && r.DateCreated == db.REVIEWs.Max(i => i.DateCreated))
                                   .Single();

            review.DateModified = DateTime.Now;
            db.REVIEWs.Remove(review);
            db.SaveChanges();

            review = db.REVIEWs.Find(review.MemberId, review.GameId);

            Assert.IsNull(review);
        }
    }
}
