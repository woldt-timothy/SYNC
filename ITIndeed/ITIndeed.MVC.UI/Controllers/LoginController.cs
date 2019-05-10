using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;

namespace ITIndeed.MVC.UI.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Logout()
        {
            Session["user"] = null;

            return View();
        }

        public ActionResult Login(string returnurl)
        {
            User user = new User();
            ViewBag.ReturnUrl = returnurl;

            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user/*, string returnurl*/)
        {
            try
            {
                if (user.UserLogin())
                {
                    ViewBag.Message = "Welcome. You're logged in.";
                    Session["user"] = user;

                    return RedirectToAction("Details", "StudentProfile");
                }
                else
                {
                    ViewBag.Message = "Sorry, wrong credentials.  Please try again.";

                    return View(user);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;

                return View(user);
            }
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
