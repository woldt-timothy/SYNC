using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;
using ITIndeed.MVC.UI.Models;
using System.Drawing;
using System.IO;

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
            EmployerStudentProfileImage espi = new EmployerStudentProfileImage();
            espi.employer = new Employer();
            espi.employer.EmployerLoadById(id);

            string filepath = Server.MapPath("~/Image/") + "pfpImage.jpg";
            System.IO.File.Delete(filepath);

            if (espi.employer.ProfilePicture != null)
            {
                MemoryStream ms = new MemoryStream(espi.employer.ProfilePicture);
                Image i = Image.FromStream(ms);
                
                i.Save(filepath);
                ms.Close();
            }

            return View(espi);
        }

        // GET: EmployerProfile/Create
        [HttpGet]
        public ActionResult Create()
        {
            EmployerStudentProfileImage espi = new EmployerStudentProfileImage();
            espi.employer = new Employer();

            return View(espi);
        }

        // POST: EmployerProfile/Create
        [HttpPost]
        public ActionResult Create(EmployerStudentProfileImage espi)
        {
            try
            {
                if(espi.ImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        espi.ImageFile.InputStream.CopyTo(ms);
                        espi.employer.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                espi.employer.EmployerInsert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(espi);
            }
        }

        // GET: EmployerProfile/Edit/5
        public ActionResult Edit(Guid id)
        {
            EmployerStudentProfileImage espi = new EmployerStudentProfileImage();
            espi.employer = new Employer();
            espi.employer.EmployerLoadById(id);

            return View(espi);
        }

        // POST: EmployerProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, EmployerStudentProfileImage espi)
        {
            try
            {
                if (espi.ImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        espi.ImageFile.InputStream.CopyTo(ms);
                        espi.employer.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                espi.employer.EmployerUpdate();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(espi);
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
