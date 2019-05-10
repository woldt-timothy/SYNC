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
            if (Authenticate.IsAuthenticated())
            {
                events = new EventList();
                events.Load();

                return View(events);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Event/Details/5
        public ActionResult Details(Guid id)
        {
            Event _event = new Event();
            _event.LoadById(id);
            _event.LoadCountOfUsersInterested();
            _event.LoadStudents();

            return View(_event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            User userEdit = new User();
            userEdit = (User)Session["user"];

            Employer employer = new Employer();
            employer.EmployerLoadUserById2(userEdit.BaseUserID);

            if (employer.EmployerId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return Redirect("Index");
            }
            else
            {
                Event _event = new Event();

                return View(_event);
            }
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

        public ActionResult AddUserInterestedInEvent(Guid id)
        {
            try
            {
                User user;

                if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Login");

                }
                else if (Session["user"] != null)
                {

                    user = new User();
                    user = (User)Session["user"];

                    Event ev = new Event();
                    ev.Id = id;
                    ev.AddUserInterestedInEvent(user.BaseUserID);

                    string route;
                    route = id.ToString();

                    return RedirectToAction("Details/" + route);

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
