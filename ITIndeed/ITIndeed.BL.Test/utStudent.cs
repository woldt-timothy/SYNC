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
            Byte[] arrBYTE = new Byte[10000];

            Student student = new Student();
            student.UserName = "SallyTheStudent";
            student.Password = "pass";
            student.StudentFirstName = "Sally";
            student.StudentLastName = "TheStudent";
            student.Email = "sallythestudent@gmail.com";
            student.FieldOfStudy = "Computer Programmer";
            student.School = "SomeSchool";
            student.Phone = "666-666-6666";
            student.ProfilePicture = arrBYTE;

            student.StudentInsert();

            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;
            int expectedUsers = 25;

            int actualUsers = users.Count();

            var students = dc.tblStudents;
            int expectedStudents = 2;

            int actualStudents = students.Count();

            Assert.AreEqual(expectedStudents + expectedUsers, actualStudents + actualUsers);
        }

        [TestMethod]
        public void LoadIDTest()
        {
            Student student = new Student();
            student.StudentLoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "Sally";
            string actual = student.StudentFirstName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Student student = new Student();
            student.StudentID = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
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
            student.StudentID = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            student.UserName = "UserName";
            student.School = "School";
            student.StudentUpdate();

            student.StudentLoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "Test123!TestSchool";
            string actual = student.UserName + student.School;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadTest()
        {
            StudentList students = new StudentList();
            students.StudentListLoad();

            int expected = 14;
            int actual = students.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadUserByIdTest()
        {
            Student student = new Student();
            student.StudentLoadUserById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "SallyTheStudent";
            string actual = student.StudentFirstName;

            Assert.AreEqual(expected, actual);
        }
    }
}
