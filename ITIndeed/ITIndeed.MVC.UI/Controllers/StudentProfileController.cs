using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;
using ITIndeed.MVC.UI.Models;

namespace ITIndeed.MVC.UI.Controllers
{

    public class StudentProfileController : Controller
    {
        StudentList students;

        // GET: StudentProfile
        public ActionResult Index()
        {
            students = new StudentList();
            students.StudentListLoad();

            return View(students);
        }

        // GET: StudentProfile/Details/5
        public ActionResult Details(Guid id)
        {
            EmployerStudentProfileImage espi = new EmployerStudentProfileImage();
            espi.student = new Student();
            espi.student.StudentLoadById(id);

            string filepath = Server.MapPath("~/pfpImageFolder/") + "pfpImage.jpg";
            System.IO.File.Delete(filepath);

            if (espi.student.ProfilePicture != null)
            {
                MemoryStream ms = new MemoryStream(espi.student.ProfilePicture);
                Image i = Image.FromStream(ms);

                i.Save(filepath);
                ms.Close();
            }

            return View(espi);
        }
        
        // GET: StudentProfile/Create
        [HttpGet]
        public ActionResult Create()
        {
            EmployerStudentProfileImage espi = new EmployerStudentProfileImage();
            espi.student = new Student();

            return View(espi);
        }

        // POST: StudentProfile/Create
        [HttpPost]
        public ActionResult Create(EmployerStudentProfileImage espi)
        {
            try
            {
                if (espi.ImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        espi.ImageFile.InputStream.CopyTo(ms);
                        espi.student.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                espi.student.StudentInsert();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(espi);
            }
        }

        // GET: StudentProfile/Edit/5
        public ActionResult Edit(Guid id)
        {
            EmployerStudentProfileImage espi = new EmployerStudentProfileImage();
            espi.student = new Student();
            espi.student.StudentLoadById(id);

            return View(espi);
        }

        // POST: StudentProfile/Edit/5
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
                        espi.student.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                espi.student.StudentUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(espi);
            }
        }

        // GET: StudentProfile/Delete/5
        public ActionResult Delete(Guid id)
        {
            Student student = new Student();
            student.StudentLoadById(id);

            return View(student);
        }

        // POST: StudentProfile/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Student s)
        {
            try
            {
                // TODO: Add delete logic here
                s.StudentDelete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }
    }
}
