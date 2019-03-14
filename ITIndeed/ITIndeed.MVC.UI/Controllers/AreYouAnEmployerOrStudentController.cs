using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITIndeed.MVC.UI.Controllers
{
    public class AreYouAnEmployerOrStudentController : Controller
    {
        // GET: AreYouAnEmployerOrStudent
        public ActionResult Index()
        {
            return View();
        }

        // GET: AreYouAnEmployerOrStudent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AreYouAnEmployerOrStudent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreYouAnEmployerOrStudent/Create
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

        // GET: AreYouAnEmployerOrStudent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AreYouAnEmployerOrStudent/Edit/5
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

        // GET: AreYouAnEmployerOrStudent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AreYouAnEmployerOrStudent/Delete/5
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
