using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITIndeed.BL;
using ITIndeed.PL;
using System.Linq;

namespace ITIndeed.BL.Test
{
    [TestClass]
    public class utEvent
    {
        [TestMethod]
        public void AddUserToEvent()
        {
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            eventObject.AddUserToEvent(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            ITIndeedEntities dc = new ITIndeedEntities();

            var eventShowings = dc.tblEventShowings;

            int expectedEventShowings = 5;

            int actualEventShowings = eventShowings.Count();

            Assert.AreEqual(expectedEventShowings, actualEventShowings);
        }

        [TestMethod]
        public void UsersAttendingEvent()
        {
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            eventObject.LoadUsers();

            int expectedUsers = 2;

            int actualUsers = eventObject.Users.Count();

            Assert.AreEqual(expectedUsers, actualUsers);
        }

        [TestMethod]
        public void StudentsAttendingEvent()
        {
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            eventObject.LoadStudents();

            int expectedStudents = 4;
            int actualStudents = eventObject.Students.Count();

            Assert.AreEqual(expectedStudents, actualStudents);
        }

        [TestMethod]
        public void LoadEventById()
        {
            Event eventObject = new Event();
            
            eventObject.LoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expectedEventName = "XYZ Networking Event";
            string actualEventName = eventObject.Name;

            Assert.AreEqual(expectedEventName, actualEventName);
        }

        [TestMethod]
        public void InsertTest()
        {
            Event eventObject = new Event();

            eventObject.StartDate = Convert.ToDateTime("12/20/2017");
            eventObject.EndDate = Convert.ToDateTime("12/21/2017");
            eventObject.Name = "EventName";
            eventObject.Type = "EventType";
            eventObject.Insert();

            ITIndeedEntities dc = new ITIndeedEntities();

            var events = dc.tblEventShowings;

            int expectedEvents = 4;
            int actualEvents = events.Count();

            Assert.AreEqual(expectedEvents, actualEvents);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Event eventObject = new Event();

            eventObject.LoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));
            eventObject.Name = "Update";
            eventObject.StartDate = Convert.ToDateTime("06/30/1987");
            eventObject.EndDate = Convert.ToDateTime("06/30/1988");
            eventObject.Type = "UpdateType";
            eventObject.Update();

            eventObject.LoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "Update6/30/1987 12:00:00 AM6/30/1988 12:00:00 AMUpdateType";
            string actual = eventObject.Name + eventObject.StartDate + eventObject.EndDate + eventObject.Type;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            eventObject.Delete();

            ITIndeedEntities dc = new ITIndeedEntities();

            var events = dc.tblEventShowings;

            var eventShowings = dc.tblEvents;

            int expected = 3 + 3;
            int actual = events.Count() + eventShowings.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadListOfEvents()
        {
            EventList events = new EventList();
            events.Load();
            
            int expected = 3;
            int actual = events.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadEventsForAUser()
        {
            EventList events = new EventList();
            events.LoadEventsForAUser(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));
            
            int expected = 2;
            int actual = events.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddUserInterestedInEvent()
        {
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            eventObject.AddUserInterestedInEvent(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            ITIndeedEntities dc = new ITIndeedEntities();

            var userInterestedInEvent = dc.tblUserInteresteds;

            int expecteduserInterestedInEvent = 7;
            int actualuserInterestedInEvent = userInterestedInEvent.Count();

            Assert.AreEqual(expecteduserInterestedInEvent, actualuserInterestedInEvent);
        }

        [TestMethod]
        public void LoadUserInterestedInEvent()
        {
            Event @event = new Event();
            @event.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            @event.LoadUsersInterested();

            int expected = 1;
            int actual = @event.Users.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadCountUserInterested()
        {
            Event @event = new Event();
            @event.Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            @event.LoadCountOfUsersInterested();

            int expected = 1;
            int actual = @event.InterestedCount;

            Assert.AreEqual(expected, actual);
        }
    }
}
