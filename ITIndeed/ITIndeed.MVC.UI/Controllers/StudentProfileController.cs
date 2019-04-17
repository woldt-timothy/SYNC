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
                ///Gage - I added the line below - it does not affect this method, all it does is call the method on line
                /// 151 to send the email upon account creation of a student
                SendEmail(espi.student.Email);

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
