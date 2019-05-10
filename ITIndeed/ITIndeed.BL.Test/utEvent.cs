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
            //event is a keyword
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("8f615527-0239-46b2-b384-7cc6905c581a");
            eventObject.AddUserToEvent(Guid.Parse("caaec2b9-84ce-478f-a916-4dc38bb7bbe9"));

            ITIndeedEntities dc = new ITIndeedEntities();

            var eventShowings = dc.tblEventShowings;

            int expectedEventShowings = 5;

            int actualEventShowings = eventShowings.Count();


            Assert.AreEqual(expectedEventShowings, actualEventShowings);//
            //test



        }


        [TestMethod]
        public void UsersAttendingEvent()
        {
            //event is a keyword
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("0d4298a4-2b7f-441a-8609-0c6cbd4e7c0e");

            eventObject.LoadUsers();

            

            int expectedUsers = 2;

            int actualUsers = eventObject.Users.Count();


            Assert.AreEqual(expectedUsers, actualUsers);



        }

        [TestMethod]
        public void StudentsAttendingEvent()
        {
            //event is a keyword
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("1468c5d6-8cca-47d5-965d-4dbdc4996691");

            eventObject.LoadStudents();



            int expectedStudents = 4;

            int actualStudents = eventObject.Students.Count();


            Assert.AreEqual(expectedStudents, actualStudents);



        }



        [TestMethod]
        public void LoadEventById()
        {
            //event is a keyword
            Event eventObject = new Event();
            
            eventObject.LoadById(Guid.Parse("0d4298a4-2b7f-441a-8609-0c6cbd4e7c0e"));

            

            string expectedEventName = "X Networking Event";

            string actualEventName = eventObject.Name;


            Assert.AreEqual(expectedEventName, actualEventName);



        }


        [TestMethod]
        public void InsertTest()
        {
            //event is a keyword
            Event eventObject = new Event();

            eventObject.StartDate = Convert.ToDateTime("12/20/2017");
            eventObject.EndDate = Convert.ToDateTime("12/21/2017");
            eventObject.Name = "Test Event";
            eventObject.Type = "Netowkring Type";
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
            //event is a keyword
            Event eventObject = new Event();


            eventObject.LoadById(Guid.Parse("0d4298a4-2b7f-441a-8609-0c6cbd4e7c0e"));

            eventObject.Name = "Update";
            eventObject.StartDate = Convert.ToDateTime("06/30/1987");
            eventObject.EndDate = Convert.ToDateTime("06/30/1988");
            eventObject.Type = "UpdateType";
            eventObject.Update();


            eventObject.LoadById(Guid.Parse("0d4298a4-2b7f-441a-8609-0c6cbd4e7c0e"));



            

            string expected = "Update6/30/1987 12:00:00 AM6/30/1988 12:00:00 AMUpdateType";


            string actual = eventObject.Name + eventObject.StartDate + eventObject.EndDate + eventObject.Type;


            Assert.AreEqual(expected, actual);



        }


        [TestMethod]
        public void DeleteTest()
        {
            //event is a keyword
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("0d4298a4-2b7f-441a-8609-0c6cbd4e7c0e");
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
            //event is a keyword
            EventList events = new EventList();
            events.Load();
            

            int expected = 3;

            int actual = events.Count();


            Assert.AreEqual(expected, actual);



        }



        [TestMethod]
        public void LoadEventsForAUser()
        {
            //event is a keyword
            EventList events = new EventList();
            events.LoadEventsForAUser(Guid.Parse("1f5f618f-1271-4ce3-9f61-67fb2a8e5c55"));
            

            int expected = 2;

            int actual = events.Count();


            Assert.AreEqual(expected, actual);



        }


        [TestMethod]
        public void AddUserInterestedInEvent()
        {
            //event is a keyword
            Event eventObject = new Event();
            eventObject.Id = Guid.Parse("5955eca0-87fc-413f-a47f-8f93436afa89");
            eventObject.AddUserInterestedInEvent(Guid.Parse("ea9ad35d-a654-4b6c-9b25-53e0b24175cd"));

            ITIndeedEntities dc = new ITIndeedEntities();

            var userInterestedInEvent = dc.tblUserInteresteds;

            int expecteduserInterestedInEvent = 7;

            int actualuserInterestedInEvent = userInterestedInEvent.Count();


            Assert.AreEqual(expecteduserInterestedInEvent, actualuserInterestedInEvent);



        }


        [TestMethod]
        public void LoadUserInterestedInEvent()
        {
            //event is a keyword
            Event @event = new Event();
            @event.Id = Guid.Parse("5955eca0-87fc-413f-a47f-8f93436afa89");
            @event.LoadUsersInterested();


            int expected = 1;

            int actual = @event.Users.Count();


            Assert.AreEqual(expected, actual);



        }


        [TestMethod]
        public void LoadCountUserInterested()
        {
            //event is a keyword
            Event @event = new Event();
            @event.Id = Guid.Parse("5955eca0-87fc-413f-a47f-8f93436afa89");
            @event.LoadCountOfUsersInterested();


            int expected = 1;

            int actual = @event.InterestedCount;


            Assert.AreEqual(expected, actual);



        }
    }
}
