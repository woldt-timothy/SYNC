using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;

namespace ITIndeed.MVC.UI.Controllers
{
    public class EmployerController : Controller
    {
        EmployerList employers;


        // GET: Employee
        public ActionResult Index()
        {

            employers = new EmployerList();
            employers.EmployerListLoad();

            return View(employers);
        }

        // GET: Employee/Details/5
        public ActionResult Details(Guid id)
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(id);

            return View(employer);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            Employer employer = new Employer();
            
            return View(employer);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employer e)
        {
            try
            {
                // TODO: Add insert logic here
                e.EmployerInsert();
                return RedirectToAction("../Content/Theme/index.html");
            }
            catch
            {
                return View(e);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(Guid id)
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(id);

            return View(employer);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Employer e)
        {
            try
            {
                // TODO: Add update logic here
                e.EmployerUpdate();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(Guid id)
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(id);

            return View(employer);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Employer e)
        {
            try
            {
                // TODO: Add delete logic here

                e.EmployerDelete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }
    }
}
