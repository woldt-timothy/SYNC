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

        // GET: EmployerProfile
        public ActionResult Index(string searchBy, string search, int pageSize = 5, int pageNumber = 1)
        {
            if (Authenticate.IsAuthenticated())
            {
                employers = new EmployerList();

                if (searchBy == "Industry")
                {
                    employers.LoadByIndustry(search, pageSize, pageNumber);
                    ViewData["Industry"] = "checked";
                    Session["searchBy"] = "Industry";
                }
                else
                {
                    employers.LoadByOrganization(search, pageSize, pageNumber);
                    ViewData["OrganizationName"] = "checked";
                    Session["searchBy"] = "OrganizationName";
                }

                // TODO FOR GAGE: Show results view for page listings
                // TODO FOR GAGE: Compress images to thumbnail size?

                if (employers.Count < pageSize)
                    ViewData["PageNextCheck"] = "hidden";
                if (pageNumber < 2)
                    ViewData["PagePreviousCheck"] = "hidden";
                
                ViewData[pageSize.ToString()] = "selected";

                Session["search"] = search;
                Session["pageSize"] = pageSize;
                Session["pageNumber"] = pageNumber;

                return View(employers);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        public ActionResult PageNext()
        {
            int pn = (int)Session["pageNumber"] + 1;
            return RedirectToAction("Index", new { searchBy = Session["searchBy"], search = Session["search"], pageSize = Session["pageSize"], pageNumber = pn });
        }

        public ActionResult PagePrevious()
        {
            int pn = (int)Session["pageNumber"] - 1;
            return RedirectToAction("Index", new { searchBy = Session["searchBy"], search = Session["search"], pageSize = Session["pageSize"], pageNumber = pn });
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
