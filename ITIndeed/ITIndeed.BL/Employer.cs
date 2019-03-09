using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIndeed.BL
{

    //Employer Inherits from User

    public class Employer : User
    {
        // Properties

        public Guid EmployerId { get; set; }
        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }
        public string Industry { get; set; }
        public Guid UserId { get; set; }









        // Constructors

        public Employer()
        {

        }

        public Employer(Guid employerId, string representativeFirstName, string representativeLastName, string phone, string email, string organizationName, string industry, Guid userId)
        {
            EmployerId = employerId;
            RepresentativeFirstName = representativeLastName;
            RepresentativeLastName = representativeLastName;
            Phone = phone;
            Email = email;
            UserId = userId;
            OrganizationName = organizationName;
            Industry = industry;
        }

        // Methods


        //Updated Fields in Method but EmployerLoadById Needs Unit Test // we also could implement inheritance and load everything into a Employer Object

        public bool EmployerLoadById(Guid employerId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == employerId).FirstOrDefault();

                    if (employer != null)
                    {
                        this.EmployerId = employer.Id;
                        this.RepresentativeFirstName = employer.RepresentativeFirstName;
                        this.RepresentativeLastName = employer.RepresentativeLastName;
                        this.Phone = employer.Phone;
                        this.Email = employer.Email;
                        this.UserId = employer.UserId;
                        this.OrganizationName = employer.OrganizationName;
                        this.Industry = employer.Industry;

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



        //***Marker For Tim //This is good to go
        public bool EmployerInsert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {

                    //Insert User, then take UserId and Insert it into Employer Table // this can all be done through employer object because employer inherits from user
                    //This code is good, Please talk with me before changing it --Thanks--Tim
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


        //Updated Fields in Method by EmployerUpdate Needs Unit Test // I think we should implement inheritance in this as well  so we can update username and password from one object

        public void EmployerUpdate()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == this.EmployerId).FirstOrDefault();

                    if (employer != null)
                    {
                        employer.RepresentativeFirstName = this.RepresentativeFirstName;
                        employer.RepresentativeLastName = this.RepresentativeLastName;
                        employer.Phone = this.Phone;
                        employer.Email = this.Email;
                        employer.Industry = this.Industry;
                        employer.OrganizationName = this.OrganizationName;
                        

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        //Needs Unit Test //This Method is also going to have to be written such that it updates the user table as well // this can all be done through the employer object
        //Since employer Inherits from User // It will basically be the opposite of EmployerInsert

        public void EmployerDelete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == this.EmployerId).FirstOrDefault();

                    if (employer != null)
                    {
                        dc.tblEmployers.Remove(employer);

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


        // Updated Fields //We also could probably implement inheritance here // Also needs unit test
        public void EmployerListLoad()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblEmployers.ToList().ForEach(e => Add(new Employer(e.Id, e.RepresentativeFirstName, e.RepresentativeLastName, e.Phone, e.Email, e.OrganizationName, e.Industry, e.UserId)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
