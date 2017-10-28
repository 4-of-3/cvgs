using CVGS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CVGS.Tests.Models
{

    [TestClass]
    public class EventModelTest
    {
        EVENT sampleEvent = new EVENT()
        {
            EventTitle = "Event Title",
            Description = "Test event description",
            EventDate = new System.DateTime(2017, 6, 23),
            ActiveStatus = true,
            Location = "Sample Location"
        };

        [TestMethod]
        public void AddEvent()
        {
            // Arrange
            CVGSEntities db = new CVGSEntities();
            EVENT foundEvent;

            // Act
            db.EVENTs.Add(sampleEvent);
            db.SaveChanges();

            foundEvent = db.EVENTs.ToList().Find(e => e == sampleEvent);

            // Assert
            Assert.IsTrue(foundEvent == sampleEvent, "AddEvent: Adding event failed");

            //Teardown
            db.EVENTs.Remove(foundEvent);
            db.SaveChanges();
        }

        [TestMethod]
        public void DeleteEvent()
        {
            // Arrange
            CVGSEntities db = new CVGSEntities();
            EVENT unFoundEvent;
            db.EVENTs.Add(sampleEvent);
            db.SaveChanges();

            if (db.GAMEs.Count() == 0)
            {
                Assert.Fail("DeleteEvent: Adding event for deletion failed");
            }

            // Act
            db.EVENTs.Remove(sampleEvent);
            db.SaveChanges();

            unFoundEvent = db.EVENTs.ToList().Find(e => e == sampleEvent);

            // Assert
            Assert.IsTrue(unFoundEvent == null);
        }
    }
}
