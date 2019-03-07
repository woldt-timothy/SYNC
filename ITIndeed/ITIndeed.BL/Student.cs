using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIndeed.BL
{
    public class Student
    {
        // Properties

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }

        // Constructors

        public Student()
        {

        }

        public Student(Guid id, string firstName, string lastName, string phone, string email, Guid userId)
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
                    tblStudent student = dc.tblStudents.Where(s => s.Id == id).FirstOrDefault();

                    if (student != null)
                    {
                        this.Id = student.Id;
                        this.FirstName = student.FirstName;
                        this.LastName = student.LastName;
                        this.Phone = student.Phone;
                        this.Email = student.Email;
                        this.UserId = student.UserId;

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

                    tblStudent student = new tblStudent();
                    this.Id = Guid.NewGuid();

                    student.Id = this.Id;
                    student.FirstName = this.FirstName;
                    student.LastName = this.LastName;
                    student.Phone = this.Phone;
                    student.Email = this.Email;
                    student.UserId = this.UserId;

                    dc.tblStudents.Add(student);
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
                    tblStudent student = dc.tblStudents.Where(s => s.Id == this.Id).FirstOrDefault();

                    if (student != null)
                    {
                        student.FirstName = this.FirstName;
                        student.LastName = this.LastName;
                        student.Phone = this.Phone;
                        student.Email = this.Email;

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
                    tblStudent student = dc.tblStudents.Where(s => s.Id == this.Id).FirstOrDefault();

                    if (student != null)
                    {
                        dc.tblStudents.Remove(student);

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

    public class StudentList : List<Student>
    {
        public void Load()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblStudents.ToList().ForEach(s => Add(new Student(s.Id, s.FirstName, s.LastName, s.Phone, s.Email, s.UserId)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
