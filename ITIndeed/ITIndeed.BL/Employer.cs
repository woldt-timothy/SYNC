using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIndeed.BL
{
    public class Employer
    {
        // Properties

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }

        // Constructors

        public Employer()
        {

        }

        public Employer(Guid id, string firstName, string lastName, string phone, string email, Guid userId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            UserId = userId;
        }

        // Methods

        public bool LoadById(Guid id)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == id).FirstOrDefault();

                    if (employer != null)
                    {
                        this.Id = employer.Id;
                        this.FirstName = employer.FirstName;
                        this.LastName = employer.LastName;
                        this.Phone = employer.Phone;
                        this.Email = employer.Email;
                        this.UserId = employer.UserId;

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
        public bool Insert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {

                    tblEmployer employer = new tblEmployer();
                    this.Id = Guid.NewGuid();

                    employer.Id = this.Id;
                    employer.FirstName = this.FirstName;
                    employer.LastName = this.LastName;
                    employer.Phone = this.Phone;
                    employer.Email = this.Email;
                    employer.UserId = this.UserId;

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
        public void Update()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == this.Id).FirstOrDefault();

                    if (employer != null)
                    {
                        employer.FirstName = this.FirstName;
                        employer.LastName = this.LastName;
                        employer.Phone = this.Phone;
                        employer.Email = this.Email;

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Delete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblEmployer employer = dc.tblEmployers.Where(e => e.Id == this.Id).FirstOrDefault();

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
        public void Load()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblEmployers.ToList().ForEach(e => Add(new Employer(e.Id, e.FirstName, e.LastName, e.Phone, e.Email, e.UserId)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
