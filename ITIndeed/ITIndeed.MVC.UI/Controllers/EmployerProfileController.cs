﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;
using ITIndeed.MVC.UI.Models;

namespace ITIndeed.MVC.UI.Controllers
{
    public class EmployerProfileController : Controller
    {

        EmployerList employers;

        // GET: EmployerProfile
        public ActionResult Index(string searchBy, string search)
        {
            if (Authenticate.IsAuthenticated())
            {
                employers = new EmployerList();
                employers.EmployerListLoad();

                if (searchBy == "Industry")
                {
                    return View(employers.Where(x => x.Industry.StartsWith(search) || search == null).ToList());
                    //return View(employers.Where(x => x.OrganizationName.StartsWith(search)).ToList());
                }
                else
                {
                    if (search == null)
                    {
                        return View(employers);
                    }
                    else
                    {
                        return View(employers.Where(x => x.OrganizationName.StartsWith(search) || search == null).ToList());
                    }
                }


                //return View(employers);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
            
        }

        // GET: EmployerProfile/Details/5
        public ActionResult Details(Guid id)
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(id);

            return View(employer);
        }

        // GET: EmployerProfile/Create
        public ActionResult Create()
        {
            Employer employer = new Employer();

            return View(employer);
        }

        // POST: EmployerProfile/Create
        [HttpPost]
        public ActionResult Create(Employer e, HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {

                }

                e.EmployerInsert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }

        // GET: EmployerProfile/Edit/5
        public ActionResult Edit(Guid id)
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(id);

            return View(employer);
        }

        // POST: EmployerProfile/Edit/5
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

        // GET: EmployerProfile/Delete/5
        public ActionResult Delete(Guid id)
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(id);

            return View(employer);
        }

        // POST: EmployerProfile/Delete/5
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
