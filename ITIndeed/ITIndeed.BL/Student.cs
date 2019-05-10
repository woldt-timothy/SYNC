using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ITIndeed.BL
{
    public class Student: User
    {
        private string field;
        public Guid StudentID { get; set; }
        [DisplayName("First Name")]
        public string StudentFirstName { get; set; }
        [DisplayName("Last Name")]
        public string StudentLastName { get; set; }
        [DisplayName("Name")]
        public string FullName { get { return StudentFirstName + " " + StudentLastName; } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        [DisplayName("Field of Study")]
        public string FieldOfStudy { get; set; }
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


        public Student()
        {

        }

        public Student(Guid studentID, string studentFirstName, string studentLastName, string phone, string email, Guid userID, string school, string fieldOfStudy, Byte[] profilePicture)
        {
            StudentID = studentID;
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
            Phone = phone;
            Email = email;
            School = school;
            FieldOfStudy = fieldOfStudy;
            UserId = userID;
            ProfilePicture = profilePicture;
        }

        public Student(Guid baseUserID, string userName, string password, string email, string field, string phone, string school, Guid userId) : base(baseUserID, userName, password)
        {
            Email = email;
            this.field = field;
            Phone = phone;
            School = school;
            UserId = userId;
        }

        //***User ID == student.UserId != student.BaseUserId // Same with UserName
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
                        this.StudentLastName = student.StudentLastName;
                        this.Phone = student.Phone;
                        this.Email = student.Email;
                        this.School = student.School;
                        this.FieldOfStudy = student.Field;
                        this.UserId = student.UserId;
                        this.UserName = user.UserName;
                        this.Password = user.Password;
                        this.ProfilePicture = (Byte[])(student.ProfilePicture);

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

        public bool StudentLoadUserById(Guid baseUserId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == baseUserId).FirstOrDefault();
                    tblStudent student = dc.tblStudents.Where(s => s.UserId == user.Id).FirstOrDefault();
                    
                    if (student != null & user != null)
                    {
                        this.StudentID = student.Id;
                        this.StudentFirstName = student.StudentFirstName;
                        this.StudentLastName = student.StudentLastName;
                        this.Phone = student.Phone;
                        this.Email = student.Email;
                        this.School = student.School;
                        this.FieldOfStudy = student.Field;
                        this.UserId = student.UserId;
                        this.UserName = user.UserName;
                        this.Password = user.Password;
                        this.ProfilePicture = (Byte[])(student.ProfilePicture);

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
        
        public bool StudentInsert()
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

                    tblStudent student = new tblStudent();
                    student.Id = Guid.NewGuid();
                    student.StudentFirstName = this.StudentFirstName;
                    student.StudentLastName = this.StudentLastName;
                    student.Phone = this.Phone;
                    student.Email = this.Email;
                    student.School = this.School;
                    student.Field = this.FieldOfStudy;
                    student.UserId = guidUserId;
                    student.ProfilePicture = this.ProfilePicture;

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
                        student.ProfilePicture = (this.ProfilePicture == null) ? student.ProfilePicture : this.ProfilePicture;

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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


    public class StudentList : List<Student>
    {
        public void StudentListLoad()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblStudents.ToList().ForEach(s => Add(new Student(s.Id, s.StudentFirstName, s.StudentLastName, s.Phone, s.Email, s.UserId, s.Field, s.School, s.ProfilePicture)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
