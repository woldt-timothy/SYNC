using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITIndeed.BL
{

        //Student Inherits from User

    public class Student: User
    {
        // Properties

        public Guid StudentID { get; set; }
        [DisplayName("First Name")]
        public string StudentFirstName { get; set; }
        [DisplayName("Last Name")]
        public string StudentLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        [DisplayName("Field of Study")]
        public string FieldOfStudy { get; set; }
        public Guid UserId { get; set; }

        // Constructors

        public Student()
        {

        }





        public Student(Guid studentID, string studentFirstName, string studentLastName, string phone, string email, Guid userID, string school, string fieldOfStudy)
        {
            StudentID = studentID;
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
            Phone = phone;
            Email = email;
            School = school;
            FieldOfStudy = fieldOfStudy;
            UserId = userID;
        }

        //***Marker For Tim //This is good to go
        //***User ID == student.UserId != student.BaseUserId // Same Goes with UserName

        public bool StudentLoadById(Guid studentID)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblStudent student = dc.tblStudents.Where(s => s.Id == studentID).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == student.UserId).FirstOrDefault();


                    if (student != null & user != null)
                    {
                        this.StudentID = student.Id;
                        this.StudentFirstName = student.StudentFirstName;
                        this.StudentLastName = student.StudentFirstName;
                        this.Phone = student.Phone;
                        this.Email = student.Email;
                        this.School = student.School;
                        this.FieldOfStudy = student.Field;
                        this.UserId = student.UserId;
                        this.UserName = user.UserName;
                        this.Password = user.Password;

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
        public bool StudentInsert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {

                    //Insert User, then take UserId and Insert it into Student Table // this can all be done through student object because student inherits from user
                    //This code is good, Please talk with me before changing it --Thanks--Tim
                    tblUser user = new tblUser();

                    user.UserName = this.UserName;
                    user.Password = this.Password;
                    this.UserInsert();


                    tblUser userGetUserId = dc.tblUsers.Where(u => u.UserName == this.UserName).FirstOrDefault();

                    Guid guidUserId = userGetUserId.Id;


                    tblStudent student = new tblStudent();
                    student.Id = Guid.NewGuid();
                    student.StudentFirstName = this.StudentFirstName;
                    student.StudentLastName = this.StudentLastName;
                    student.Phone = this.Phone;
                    student.Email = this.Email;
                    student.School = this.School;
                    student.Field = this.FieldOfStudy;
                    student.UserId = guidUserId;

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


        //***This is good to go
        // Please talk with me before changing it --Thanks--Tim
        public void StudentUpdate()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblStudent student = dc.tblStudents.Where(s => s.Id == this.StudentID).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == student.UserId).FirstOrDefault();

                    if (student != null && user != null)
                    {
                        student.StudentFirstName = (this.StudentFirstName == null) ? student.StudentFirstName: this.StudentFirstName;
                        student.StudentLastName = (this.StudentLastName == null) ? student.StudentLastName: this.StudentLastName;
                        student.Phone = this.Phone = (this.Phone == null) ? student.Phone : this.Phone;
                        student.Email = (this.Email == null) ? student.Email : this.Email;
                        student.School = (this.School == null) ? student.School : this.School;
                        student.Field = (this.FieldOfStudy == null) ? student.Field : this.FieldOfStudy;
                        user.UserName = (this.UserName == null) ? user.UserName : this.UserName;
                        user.Password = (this.Password == null) ? user.Password : this.Password;

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //***Marker For Tim //This is good to go
        public void StudentDelete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblStudent student = dc.tblStudents.Where(s => s.Id == this.StudentID).FirstOrDefault();
                    tblUser user = dc.tblUsers.Where(u => u.Id == student.UserId).FirstOrDefault();

                    if (student != null)
                    {
                        dc.tblStudents.Remove(student);
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


    //***Marker //This is good to go
    public class StudentList : List<Student>
    {
        public void StudentListLoad()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblStudents.ToList().ForEach(s => Add(new Student(s.Id, s.StudentFirstName, s.StudentLastName, s.Phone, s.Email, s.UserId, s.Field, s.School)));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
