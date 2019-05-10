using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
            User user;

            if (id == null & Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (Session["user"] != null)
            {
                user = new User();
                user = (User)Session["user"];

                Student student = new Student();
                student.StudentLoadUserById(user.BaseUserID);

                if (student.Email != null)
                {
                    return View(student);
                }
                else
                {
                    Employer employer = new Employer();
                    employer.EmployerLoadUserById(user.BaseUserID);

                    return RedirectToAction("Details", "Employer", new { id = employer.EmployerId });
                }
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
                SendEmail(s.Email);

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

                return RedirectToAction("Details" + id);
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

        public bool SendEmail(string Email)
        {
            try
            {

                string toEmail, subject, emailBody;
                toEmail = Email;
                subject = "Thanks for signing up with Sync!";
                emailBody = "Thanks for signing up with Sync!";


                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                
                client.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
