using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITIndeed.BL
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public List<User> Users { get; set; }
        public List<Student> Students { get; set; }
        public string Location { get; set; }
        public int InterestedCount { get; set; }

        public Event()
        {

        }

        public Event(Guid id, string name, string type, DateTime startDate, DateTime endDate, Guid userId)
        {
            Id = id;
            Name = name;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
        }

        public Event(Guid id, string name, DateTime startDate, DateTime endDate, string type)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
        }

        public bool AddUserInterestedInEvent(Guid userId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUserInterested eventInterested = dc.tblUserInteresteds.Where(e => (e.UserId == userId) && (e.EventId == this.Id)).FirstOrDefault(); // Check to see if user already joined event

                    if (eventInterested == null)
                    {
                        eventInterested = new tblUserInterested();
                        eventInterested.Id = Guid.NewGuid();
                        eventInterested.UserId = userId;
                        eventInterested.EventId = this.Id;

                        dc.tblUserInteresteds.Add(eventInterested);
                        dc.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false; // User already interested in event.
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadUsersInterested()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    Users = new List<User>();

                    var eventInteresteds = dc.tblUserInteresteds.Where(e => e.EventId == this.Id);

                    foreach (var eventInterested in eventInteresteds)
                    {
                        tblUser user = dc.tblUsers.Where(u => u.Id == eventInterested.UserId).FirstOrDefault();
                        Users.Add(new User(user.Id, user.UserName));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadCountOfUsersInterested()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    var count = dc.tblUserInteresteds.Where(e => e.EventId == this.Id).Count(); // User already joined event

                    this.InterestedCount = count;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddUserToEvent(Guid userId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEventShowing eventShowing = dc.tblEventShowings.Where(e => (e.UserId == userId) && (e.EventId == this.Id)).FirstOrDefault();

                    if (eventShowing == null)
                    {
                        eventShowing = new tblEventShowing();
                        eventShowing.Id = Guid.NewGuid();
                        eventShowing.UserId = userId;
                        eventShowing.EventId = this.Id;

                        dc.tblEventShowings.Add(eventShowing);
                        dc.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false; // User already attending event.
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadUsers() // Load users attending event
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    Users = new List<User>();

                    var eventShowings = dc.tblEventShowings.Where(e => e.EventId == this.Id);
                    if (eventShowings != null)
                    {
                        foreach (var eventShowing in eventShowings)
                        {
                            tblUser user = dc.tblUsers.Where(u => u.Id == eventShowing.UserId).FirstOrDefault();
                            if (user != null)
                            {
                                Users.Add(new User(user.Id, user.UserName));
                            }
                            
                        }
                    }
                 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadStudents() // Load student attending event
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    Students = new List<Student>();

                    var eventShowings = dc.tblEventShowings.Where(e => e.EventId == this.Id);
                    if (eventShowings != null)
                    {
                        foreach (var eventShowing in eventShowings)
                        {
                            tblUser user = dc.tblUsers.Where(u => u.Id == eventShowing.UserId).FirstOrDefault();
                            tblStudent student = dc.tblStudents.Where(s => s.UserId == user.Id).FirstOrDefault();
                            if (student != null)
                            {
                                Students.Add(new Student(student.Id, student.StudentFirstName, student.StudentLastName, student.Phone, student.Email, student.UserId, student.School, student.Field, student.ProfilePicture));
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool LoadById(Guid id)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEvent tevent = dc.tblEvents.Where(e => e.Id == id).FirstOrDefault();

                    if (tevent != null)
                    {
                        this.Id = tevent.Id;
                        this.Name = tevent.Name;
                        this.Type = tevent.Type;
                        this.StartDate = tevent.StartDate;
                        this.EndDate = tevent.EndDate;
                        //Gage - Error Came from trying to pull a UserId from the Event Table, the user ID is part of the tblEventShowing Junction table -- i.e. A user can have many events and an event can have many users // otherwise looks good    

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool Insert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEvent tevent = new tblEvent();
                    tevent.Id = Guid.NewGuid();
                    tevent.Name = this.Name;
                    tevent.Type = this.Type;
                    tevent.StartDate = Convert.ToDateTime(this.StartDate);
                    tevent.EndDate = Convert.ToDateTime(this.EndDate);
                    //the error was the same thing as above all good now :)           

                    dc.tblEvents.Add(tevent);
                    dc.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Update()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEvent tevent = dc.tblEvents.Where(e => e.Id == this.Id).FirstOrDefault();

                    if (tevent != null)
                    {
                        tevent.Name = this.Name;
                        tevent.Type = this.Type;
                        tevent.StartDate = this.StartDate;
                        tevent.EndDate = this.EndDate;

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void Delete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEvent tevent = dc.tblEvents.Where(e => e.Id == this.Id).FirstOrDefault();

                    if (tevent != null)
                    {
                        var eventShowings = dc.tblEventShowings.Where(e => e.EventId == this.Id);

                        dc.tblEventShowings.RemoveRange(eventShowings);
                        dc.tblEvents.Remove(tevent);

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public class EventList : List<Event>
    {
        public void Load()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {

                    dc.tblEvents.ToList().ForEach(e => Add(new Event(e.Id, e.Name, e.StartDate, e.EndDate, e.Type)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Better way to do this?
        public void LoadEventsForAUser(Guid id)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    var guidList = new List<Guid>();

                    dc.tblEventShowings.Where(es => es.UserId == id).ToList().ForEach(es => guidList.Add(es.EventId));

                    foreach (Guid g in guidList)

                    {
                        dc.tblEvents.Where(e => e.Id == g).ToList().ForEach(e => Add(new Event(g, e.Name, e.Type, e.StartDate, e.EndDate, id)));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        ///NOTES///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Gage - This Method Did not Work - We haven't given the user the ability to add an event yet so I wasn't sure what this method does, I left the code though


        // Load events the user has created.
        //public void LoadByUser(Guid id)
        //{
        //    try
        //    {
        //        using (ITIndeedEntities dc = new ITIndeedEntities())
        //        {
        //            this.Clear();
        //            dc.tblEvents.Where(e => e.UserId == id).ToList().ForEach(e => Add(new Event(e.Id, e.Name, e.Type, e.StartDate, e.EndDate, e.UserId)));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        ///NOTES
        //Load events the user is attending. --- The LoadEventsForAUser(Guid id) I created based off of your code accomplishes this though -- so no need to rewrite this one - thanks - tim

        //This method also did not work, but I left the code

        //public void LoadAttending(Guid id)
        //{
        //    try
        //    {
        //        using (ITIndeedEntities dc = new ITIndeedEntities())
        //        {
        //            this.Clear();
        //            List<tblEventShowing> showings = dc.tblEventShowings.Where(es => es.UserId == id).ToList();

        //            foreach (var showing in showings)
        //            {
        //                tblEvent _event = dc.tblEvents.Where(e => e.Id == showing.EventId).FirstOrDefault();

        //                this.Add(new Event(_event.Id, _event.Name, _event.Type, _event.StartDate, _event.EndDate, _event.UserId));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
