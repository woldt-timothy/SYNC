using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITIndeed.BL;
using ITIndeed.PL;
using System.Linq;

namespace ITIndeed.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void InsertTest()
        {
            //Tests to see if both records were inserted


            Student student = new Student();
            student.UserName = "tim5";
            student.Password = "maple";
            student.StudentFirstName = "tim5";
            student.StudentLastName = "Clyde444";
            student.Email = "bonnytheman@fvtc,edu444";
            student.FieldOfStudy = "Computer Engineering444";
            student.School = "Fox Valley Technical College444";
            student.Phone = null;
            
            student.StudentInsert();


            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;

            int expectedUsers = 10;

            int actualUsers = users.Count();

            

            var students = dc.tblStudents;

            int expectedStudents = 7;

            int actualStudents = students.Count();

            Assert.AreEqual(expectedStudents + expectedUsers, actualStudents + actualUsers);


        }


        [TestMethod]
        public void LoadIDTest()
        {



            Student student = new Student();
            student.StudentLoadById(Guid.Parse("c6c3c4be-7562-4067-84b9-ee496875bd33"));


            string expected = "tim4";
            string actual = student.StudentFirstName;



            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void DeleteTest()
        {

            Student student = new Student();
            student.StudentID = Guid.Parse("c6c3c4be-7562-4067-84b9-ee496875bd33");
            student.StudentDelete();


            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;

            int expectedUsers = 19;

            int actualUsers = users.Count();



            var students = dc.tblStudents;

            int expectedStudents = 14;

            int actualStudents = students.Count();

            Assert.AreEqual(expectedStudents + expectedUsers, actualStudents + actualUsers);


        }


        [TestMethod]
        public void UpdateTest()
        {

            Student student = new Student();
            student.StudentID = Guid.Parse("d3061ddd-9cc3-4998-adef-1f0b8ded03cd");
            student.UserName = "Test123!";
            student.School= "TestSchool";
            student.StudentUpdate();


            student.StudentLoadById(Guid.Parse("d3061ddd-9cc3-4998-adef-1f0b8ded03cd"));

            string expected = "Test123!TestSchool";
            string actual = student.UserName + student.School;

            Assert.AreEqual(expected, actual);


        }





        [TestMethod]
        public void LoadTest()
        {
            //Tests to if Number of Students is Equal to Number of Employees in Database


            StudentList students = new StudentList();
            students.StudentListLoad();

            int expected = 14;
            int actual = students.Count();



            Assert.AreEqual(expected, actual);


        }

    }
}
