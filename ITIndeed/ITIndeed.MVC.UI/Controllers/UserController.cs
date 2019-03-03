using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using ITIndeed.BL;
using ITIndeed.MVC.UI.Models;



namespace ITIndeed.MVC.UI.Controllers
{
    public class UserController : Controller
    {

        UserList users;

        // GET: User
        public ActionResult Index()
        {
           
                users = new UserList();
                users.Load();
                return View(users);
          
        }

        // GET: User/Details/5
        public ActionResult Details(Guid id)
        {
            User user = new User();
            user.LoadById(id);

            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // TODO: Add insert logic here
                user.Insert();
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid id)
        {
            User user = new User();
            user.LoadById(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, User user)
        {
            try
            {
                // TODO: Add update logic here
                user.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid id)
        {
            User user = new User();
            user.LoadById(id);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, User user)
        {
            try
            {
                // TODO: Add delete logic here
                user.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }
    }
}
