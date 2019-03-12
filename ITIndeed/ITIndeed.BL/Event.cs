﻿using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIndeed.BL
{
    public class Event
    {
        // Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<User> Users { get; set; }

        // Constructors

        public Event()
        {

        }

        public Event(Guid id, string name, string type, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
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
        public void LoadUsers(Guid id) // Load list of users attending the event
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    Users = new List<User>();

                    var eventShowings = dc.tblEventShowings.Where(e => e.EventId == id);

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
                    this.Id = Guid.NewGuid();

                    tevent.Id = this.Id;
                    tevent.Name = this.Name;
                    tevent.Type = this.Type;
                    tevent.StartDate = this.StartDate;
                    tevent.EndDate = this.EndDate;

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
                    dc.tblEvents.ToList().ForEach(e => Add(new Event(e.Id, e.Name, e.Type, e.StartDate, e.EndDate)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}