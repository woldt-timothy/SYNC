using ITIndeed.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.MVC.UI.Models;


namespace ITIndeed.MVC.UI.Controllers
{
    public class EventController : Controller
    {
        EventList events;

        // GET: Event
        public ActionResult Index()
        {
            events = new EventList();
            events.Load();

            return View(events);
        }

        // GET: Event/Details/5
        public ActionResult Details(Guid id)
        {
            Event _event = new Event();
            _event.LoadById(id);
            _event.LoadUsers();
            return View(_event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            Event _event = new Event();
            return View(_event);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event e)
        {
            try
            {
                // TODO: Add insert logic here
                e.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }

        // GET: Event/Edit/5
        public ActionResult Edit(Guid id) // TODO: Maybe add a way for the user that created the event to remove people from the event and they wont be able to rejoin the event.
        {
            Event _event = new Event();
            _event.LoadById(id);
            _event.LoadUsers();

            return View(_event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Event e)
        {
            try
            {
                // TODO: Add update logic here
                e.Update();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(Guid id)
        {
            Event _event = new Event();
            _event.LoadById(id);

            return View(_event);
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Event e)
        {
            try
            {
                // TODO: Add delete logic here
                e.Delete();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }


        
        public ActionResult AddUserToEvent(Guid id)
        {            
            try
            {
                if (Authenticate.IsAuthenticated())
                {
                    Event ev = new Event();
                    ev.Id = id;
                    User user = new User();
                    user = (User)Session["user"];
                    ev.AddUserToEvent(user.BaseUserID);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ActionResult LoadListOfUsersAttendingEvent(Guid id)
        {
            try
            {
                if (Authenticate.IsAuthenticated())
                {

                    Event ev = new Event();
                    ev.Id = id;
                    ev.LoadUsers();
                    
                    
                    return RedirectToAction("Details");
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
