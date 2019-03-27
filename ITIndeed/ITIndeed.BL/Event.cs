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
        // Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public List<User> Users { get; set; }

        // Constructors

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

        // Methods


        public bool AddUserToEvent(Guid userId) // Add a user to this event
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEventShowing eventShowing = dc.tblEventShowings.Where(e => (e.UserId == userId) && (e.EventId == this.Id)).FirstOrDefault(); // Check to see if user already joined event

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
                        return false; // User is already attending event.
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void LoadUsers() // Load list of users attending the event
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    Users = new List<User>();

                    var eventShowings = dc.tblEventShowings.Where(e => e.EventId == this.Id);

                    foreach (var eventShowing in eventShowings)
                    {
                        tblUser user = dc.tblUsers.Where(u => u.Id == eventShowing.UserId).FirstOrDefault();
                        Users.Add(new User(user.Id, user.UserName));
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
                        //Gage The Error Came from trying to pull a UserId from the Event Table, the user ID is part of the tblEventShowing Junction table -- i.e. A user can have many events and an event can have many users // otherwise looks good    

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

        // There might be a better way to do this --- But it works see unit test
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
