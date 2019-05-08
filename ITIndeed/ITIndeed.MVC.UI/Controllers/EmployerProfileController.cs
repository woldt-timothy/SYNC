using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIndeed.BL;
using ITIndeed.MVC.UI.Models;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace ITIndeed.MVC.UI.Controllers
{
    public class EmployerProfileController : Controller
    {

        EmployerList employers;
        List<Employer> filteredEmployers;
        //int pageSize;
        //int pageNumber;

        // GET: EmployerProfile
        public ActionResult Index(string searchBy, string search, int pageSize = 5, int pageNumber = 1)
        {
            if (Authenticate.IsAuthenticated())
            {
                /*if (pageSize == 0)
                    pageSize = 5;
                if (size != null)
                    pageSize = size;

                if (pageSize == null)
                    pageSize = 5;*/

                if (employers == null)
                {
                    employers = new EmployerList();
                    employers.EmployerListLoad();
                }

                if (filteredEmployers == null)
                {
                    filteredEmployers = new List<Employer>();
                    filteredEmployers = employers;
                }

                if (searchBy == "Industry")
                {
                    filteredEmployers = employers.Where(x => x.Industry.StartsWith(search) || search == null).ToList();
                }
                else if(search != null)
                {
                    filteredEmployers = employers.Where(x => x.OrganizationName.StartsWith(search) || search == null).ToList();
                }

                if (filteredEmployers.Count < pageSize)
                    pageSize = filteredEmployers.Count;
                
                filteredEmployers = filteredEmployers.GetRange(pageSize * (pageNumber - 1), pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;

                return View(filteredEmployers);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
            
        }

        // GET: EmployerProfile/Details/5
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

                string tempString;
                tempString = id.ToString();

                Guid tempGuid;
                tempGuid = Guid.Parse(tempString);

                Employer employer = new Employer();
                employer.EmployerLoadById(tempGuid);
                return View(employer);

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            }

        // GET: EmployerProfile/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Employer());
        }

        // POST: EmployerProfile/Create
        [HttpPost]
        public ActionResult Create(Employer e)
        {
            try
            {
                if (e.UploadedImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        e.UploadedImageFile.InputStream.CopyTo(ms);
                        e.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                e.EmployerInsert();
                SendEmail(e.Email);
                
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
            User userEdit = new User();
            userEdit = (User)Session["user"];

            Employer employerEdit = new Employer();
            employerEdit.EmployerLoadById(id);

            if (employerEdit.UserId != userEdit.BaseUserID)
            {
                return RedirectToAction("Details/" + id);
            }
            else
            {
                Employer e = new Employer();
                e.EmployerLoadById(id);

                return View(e);
            }
            
        }

        // POST: EmployerProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Employer e)
        {

            try
            {
                if (e.UploadedImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        e.UploadedImageFile.InputStream.CopyTo(ms);
                        e.ProfilePicture = ms.GetBuffer();
                        ms.Close();
                    }
                }

                e.EmployerUpdate(id);

                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return View(e);
            }
        }

        // GET: EmployerProfile/Delete/5
        public ActionResult Delete(Guid id)
        {
            Employer e = new Employer();
            e.EmployerLoadById(id);

            return View(e);
        }

        // POST: EmployerProfile/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Employer e)
        {
            try
            {
                e.EmployerDelete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
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
