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
            student.UserName = "tim3";
            student.Password = "maple";
            student.StudentFirstName = "Bonny444";
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
    }
}
