using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Drawing;

namespace ITIndeed.BL
{
    public class Employer : User
    {
        public Guid EmployerId { get; set; }
        [DisplayName("First Name")]
        public string RepresentativeFirstName { get; set; }
        [DisplayName("Last Name")]
        public string RepresentativeLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Organization Name")]
        public string OrganizationName { get; set; }
        public string Industry { get; set; }
        public Guid UserId { get; set; }
        public Byte[] ProfilePicture { get; set; }
        public string ProfilePictureView
        {
            get
            {
                if (ProfilePicture != null)
                {
                    return "data:image/jpeg;base64," + Convert.ToBase64String(ProfilePicture);
                }
                else
                {
                    return "/Images/blank-profile-picture.jpg";
                }
            }
        }
        public HttpPostedFileBase UploadedImageFile { get; set; }

        public Employer()
        {

        }

        public Employer(Guid employerId, string representativeFirstName, string representativeLastName, string phone, string email, string organizationName, string industry, Guid userId, Byte[] profilePicture)
        {
            EmployerId = employerId;
            RepresentativeFirstName = representativeFirstName;
            RepresentativeLastName = representativeLastName;
            Phone = phone;
            Email = email;
            UserId = userId;
            OrganizationName = organizationName;
            Industry = industry;
            ProfilePicture = profilePicture;
        }

        public void LoadUserImage()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    this.ProfilePicture = dc.tblEmployers.Where(e => e.Id == this.EmployerId).FirstOrDefault().ProfilePicture;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //***User ID == employer.UserId != employer.BaseUserId // Same Goes with UserName
        public bool EmployerLoadById(Guid employerId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == employerId).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == employer.UserId).FirstOrDefault();

                    if (employer != null & user != null)
                    {
                        this.EmployerId = employer.Id;
                        this.RepresentativeFirstName = employer.RepresentativeFirstName;
                        this.RepresentativeLastName = employer.RepresentativeLastName;
                        this.Phone = employer.Phone;
                        this.Email = employer.Email;
                        this.OrganizationName = employer.OrganizationName;
                        this.Industry = employer.Industry;
                        this.Password = user.Password;
                        this.UserName = user.UserName;
                        this.UserId = employer.UserId;
                        this.ProfilePicture = employer.ProfilePicture;

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //this is just a quick hack - we can fix this later - tim
        public bool EmployerLoadUserById2(Guid userId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.UserId == userId).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == userId).FirstOrDefault();

                    if (employer != null & user != null)
                    {
                        this.EmployerId = employer.Id;
                        this.RepresentativeFirstName = employer.RepresentativeFirstName;
                        this.RepresentativeLastName = employer.RepresentativeLastName;
                        this.Phone = employer.Phone;
                        this.Email = employer.Email;
                        this.OrganizationName = employer.OrganizationName;
                        this.Industry = employer.Industry;
                        this.Password = user.Password;
                        this.UserName = user.UserName;
                        this.UserId = employer.UserId;
                        this.ProfilePicture = employer.ProfilePicture;


                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EmployerLoadUserById(Guid userId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.UserId == userId).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == employer.UserId).FirstOrDefault();

                    if (employer != null & user != null)
                    {
                        this.EmployerId = employer.Id;
                        this.RepresentativeFirstName = employer.RepresentativeFirstName;
                        this.RepresentativeLastName = employer.RepresentativeLastName;
                        this.Phone = employer.Phone;
                        this.Email = employer.Email;
                        this.OrganizationName = employer.OrganizationName;
                        this.Industry = employer.Industry;
                        this.Password = user.Password;
                        this.UserName = user.UserName;
                        this.UserId = employer.UserId;
                        this.ProfilePicture = employer.ProfilePicture;


                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EmployerInsert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = new tblUser();

                    user.UserName = this.UserName;
                    user.Password = this.Password;
                    this.UserInsert();

                    tblUser userGetUserId = dc.tblUsers.Where(u => u.UserName == this.UserName).FirstOrDefault();
                    Guid guidUserId = userGetUserId.Id;

                    tblEmployer employer = new tblEmployer();
                    employer.Id = Guid.NewGuid();
                    employer.RepresentativeFirstName= this.RepresentativeFirstName;
                    employer.RepresentativeLastName = this.RepresentativeLastName;
                    employer.Phone = this.Phone;
                    employer.Email = this.Email;
                    employer.OrganizationName = this.OrganizationName;
                    employer.Industry = this.Industry;
                    employer.UserId = guidUserId;
                    employer.ProfilePicture = this.ProfilePicture;

                    dc.tblEmployers.Add(employer);
                    dc.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EmployerUpdate(Guid employerId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == employerId).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == employer.UserId).FirstOrDefault();

                    if (employer != null && user != null)
                    {
                        employer.RepresentativeFirstName = (this.RepresentativeFirstName == null) ? employer.RepresentativeFirstName : this.RepresentativeFirstName;
                        employer.RepresentativeLastName = (this.RepresentativeLastName == null) ? employer.RepresentativeLastName : this.RepresentativeLastName;
                        employer.Phone = (this.Phone == null) ? employer.Phone: this.Phone;
                        employer.Email = (this.Email == null) ? employer.Email : this.Email;
                        employer.Industry = (this.Industry == null) ? employer.Industry : this.Industry;
                        employer.OrganizationName = (this.OrganizationName == null) ? employer.OrganizationName: this.OrganizationName;
                        user.UserName = (this.UserName == null) ? user.UserName : this.UserName;
                        user.Password = (this.Password == null) ? user.Password: this.Password;
                        employer.ProfilePicture = (this.ProfilePicture == null) ? employer.ProfilePicture: this.ProfilePicture;

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmployerDelete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == this.EmployerId).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == employer.UserId).FirstOrDefault();
                    if (employer != null & user != null)
                    {
                        dc.tblEmployers.Remove(employer);
                        dc.tblUsers.Remove(user);

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


    public class EmployerList : List<Employer>
    {
        public int TotalCount { get; set; }

        //this is what is taking so long // cache on server?
        public void EmployerListLoad()
        {
            try
            {
                this.Clear();

                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblEmployers.ToList().ForEach(e => Add(new Employer(e.Id, e.RepresentativeFirstName, e.RepresentativeLastName, e.Phone, e.Email, e.OrganizationName, e.Industry, e.UserId, e.ProfilePicture)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        ///////////////////////////Notes for Gage/////*****//////
        ///
        /*public void LoadByPaging(int pageSize, int pageNumber)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblEmployers
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList().ForEach(e => Add(new Employer(e.Id, e.RepresentativeFirstName, e.RepresentativeLastName, e.Phone, e.Email, e.OrganizationName, e.Industry, e.UserId)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/

        // pageSize * (pageNumber - 1) <-- gages fancy equation

        public void LoadByOrganization(string input, int pageSize, int pageNumber)
        {
            try
            {
                this.Clear();

                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    //TotalCount = dc.tblEmployers.Where(e => e.OrganizationName.ToUpper().Contains(input.ToUpper()) || string.IsNullOrEmpty(input)).Count();

                    dc.tblEmployers
                        .Where(e => e.OrganizationName.ToUpper().Contains(input.ToUpper()) || string.IsNullOrEmpty(input))
                        .OrderBy(e => e.OrganizationName)
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList().ForEach(e => Add(new Employer(e.Id, e.RepresentativeFirstName, e.RepresentativeLastName, e.Phone, e.Email, e.OrganizationName, e.Industry, e.UserId, e.ProfilePicture)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadByIndustry(string input, int pageSize, int pageNumber)
        {
            try
            {
                this.Clear();

                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    //TotalCount = dc.tblEmployers.Where(e => e.Industry.ToUpper().Contains(input.ToUpper()) || string.IsNullOrEmpty(input)).Count();

                    dc.tblEmployers
                        .Where(e => e.Industry.ToUpper().Contains(input.ToUpper()) || string.IsNullOrEmpty(input))
                        .OrderBy(e => e.Industry)
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList().ForEach(e => Add(new Employer(e.Id, e.RepresentativeFirstName, e.RepresentativeLastName, e.Phone, e.Email, e.OrganizationName, e.Industry, e.UserId, e.ProfilePicture)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ClearUserImages()
        {
            try
            {
                this.ForEach(e => e.ProfilePicture = null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
