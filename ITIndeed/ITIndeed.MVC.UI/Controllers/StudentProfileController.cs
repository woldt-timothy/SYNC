using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

                    if(student.Email != null)
                    {
                    return View(student);
                    }
                    else
                    {
                    Employer employer = new Employer();
                    employer.EmployerLoadUserById(user.BaseUserID);
                    return RedirectToAction("Details", "EmployerProfile", new { id = employer.EmployerId });
                }

                    
                //}
                //}
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            
        }
        
        // GET: StudentProfile/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Student());
        }

        // POST: StudentProfile/Create
        [HttpPost]
        public ActionResult Create(Student s)
        {
            try
            {
                if (s.UploadedImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.UploadedImageFile.InputStream.CopyTo(ms);
                        s.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                s.StudentInsert();
                SendEmail(s.Email);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: StudentProfile/Edit/5
        public ActionResult Edit(Guid id)
        {
            Student s = new Student();
            s.StudentLoadById(id);

            return View(s);
        }

        // POST: StudentProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Student s)
        {
            try
            {
                if (s.UploadedImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.UploadedImageFile.InputStream.CopyTo(ms);
                        s.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                s.StudentUpdate();

                return RedirectToAction("Details");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: StudentProfile/Delete/5
        public ActionResult Delete(Guid id)
        {
            Student s = new Student();
            s.StudentLoadById(id);

            return View(s);
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
                //mailMessage.IsBodyHtml = true;
                //mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;



            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
