using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;
using ITIndeed.MVC.UI.Models;


namespace ITIndeed.MVC.UI.Controllers
{
    public class StudentController : Controller
    {

        StudentList students;

        // GET: Student
        public ActionResult Index()
        {
            students = new StudentList();
            students.StudentListLoad();

            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(Guid? id)
        {
            //Guid guid = new Guid();
            User user;


            if (id == null & Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            else if (Session["user"] != null)
            {
                //if (Authenticate.IsAuthenticated())
                //   {
                user = new User();
                user = (User)Session["user"];

                //if (user == null)
                //{
                //    return RedirectToAction("Login", "Login");
                //}
                //else
                //{
                    Student student = new Student();
                    student.StudentLoadUserById(user.BaseUserID);
                    return View(student);
                //}
                //}
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            
        }


        // GET: Student/Create
        public ActionResult Create()
        {
            Student student = new Student();
            return View(student);
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student s)
        {
            try
            {
                // TODO: Add insert logic here
                s.StudentInsert();
                return RedirectToAction("../Content/Theme/index.html");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(Guid id)
        {

            Student student = new Student();
            student.StudentLoadById(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Student s)
        {
            try
            {
                // TODO: Add update logic here
                s.StudentUpdate();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(Guid id)
        {
            Student student = new Student();
            student.StudentLoadById(id);

            return View(student);
        }

        // POST: Student/Delete/5
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
