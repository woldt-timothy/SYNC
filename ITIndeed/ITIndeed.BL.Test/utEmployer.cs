using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITIndeed.BL;
using ITIndeed.PL;
using System.Linq;

namespace ITIndeed.BL.Test
{
    [TestClass]
    public class utEmployer
    {
        [TestMethod]
        public void InsertTest()
        {
            Employer employer = new Employer();
            Byte[] arrBYTE = new Byte[10000];

            employer.UserName = "JoeJoe";
            employer.Password = "pass";
            employer.RepresentativeFirstName= "Joe";
            employer.RepresentativeLastName= "Joe";
            employer.Email = "jojo4597467664@gmail.com";
            employer.Industry = "Clowning";
            employer.OrganizationName = "Clown Store";
            employer.Phone = "888-888-8888";
            employer.ProfilePicture = arrBYTE;
            
            employer.EmployerInsert();

            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;
            int expectedUsers = 8;
            int actualUsers = users.Count();


            var employers = dc.tblEmployers;

            int expectedEmployers = 5;

            int actualEmployers = employers.Count();

            Assert.AreEqual(expectedEmployers + expectedUsers, actualEmployers + actualUsers);
        }

        [TestMethod]
        public void LoadTest()
        {
            EmployerList employers = new EmployerList();
            employers.EmployerListLoad();

            int expected = 6;
            int actual = employers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadIDTest()
        {
            Employer employer = new Employer();
            employer.EmployerLoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "Joe";
            string actual = employer.RepresentativeFirstName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Employer employer = new Employer();
            employer.EmployerId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            employer.EmployerDelete();

            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;

            int expectedUsers = 17;

            int actualUsers = users.Count();


            var employers = dc.tblEmployers;

            int expectedEmployers = 5;

            int actualEmployers = employers.Count();

            Assert.AreEqual(expectedEmployers + expectedUsers, actualEmployers + actualUsers);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Employer employer = new Employer();
            employer.EmployerId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            employer.UserName = "NEWUSERNAME";
            employer.Industry = "NEWINDUSTRY";

            employer.EmployerLoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "NEWUSERNAMENEWINDUSTRY";
            string actual = employer.UserName + employer.Industry;

            Assert.AreEqual(expected, actual);
        }
    }
}
