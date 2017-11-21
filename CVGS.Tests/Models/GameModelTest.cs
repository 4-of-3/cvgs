using CVGS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CVGS.Tests.Models
{

    [TestClass]
    public class GameModelTest
    {
        GAME sampleGame = new GAME()
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

        [TestMethod]
        public void AddGame()
        {
            // Arrange
            CVGSEntities db = new CVGSEntities();
            GAME foundGame;

            // Act
            db.GAMEs.Add(sampleGame);
            db.SaveChanges();

            foundGame = db.GAMEs.ToList().Find(g => g == sampleGame);

            // Assert
            Assert.IsTrue(foundGame == sampleGame, "AddGame: Adding game failed");

            //Teardown
            db.GAMEs.Remove(foundGame);
            db.SaveChanges();
        }

        [TestMethod]
        public void DeleteGame()
        {
            // Arrange
            CVGSEntities db = new CVGSEntities();
            GAME unFoundGame;
            db.GAMEs.Add(sampleGame);
            db.SaveChanges();

            if (db.GAMEs.Count() == 0)
            {
                Assert.Fail("DeleteGame: Adding game for deletion failed");
            }

            // Act
            db.GAMEs.Remove(sampleGame);
            db.SaveChanges();

            unFoundGame = db.GAMEs.ToList().Find(g => g == sampleGame);

            // Assert
            Assert.IsTrue(unFoundGame == null);
        }
    }
}
