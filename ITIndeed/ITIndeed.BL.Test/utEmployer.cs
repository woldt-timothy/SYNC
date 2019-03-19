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
            //Tests to see if both records were inserted


            Employer employer = new Employer();




            employer.UserName = "foxValleyBadgerMan3";
            employer.Password = "maple";
            employer.RepresentativeFirstName= "Bonny443";
            employer.RepresentativeLastName= "Clyde443";
            employer.Email = "bonnytheman@bemis.com3";
            employer.Industry = "Retail3";
            employer.OrganizationName = "Bemis Company3";
            employer.Phone = null;
            

            employer.EmployerInsert();


            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;

            int expectedUsers = 11;

            int actualUsers = users.Count();



            var employers = dc.tblEmployers;

            int expectedEmployers = 4;

            int actualEmployers = employers.Count();

            Assert.AreEqual(expectedEmployers + expectedUsers, actualEmployers + actualUsers);


        }

        [TestMethod]
        public void LoadTest()
        {
            //Tests to if Number of Employees is Equal to Number of Employees in Database


            EmployerList employers = new EmployerList();
            employers.EmployerListLoad();

            int expected = 6;
            int actual = employers.Count();



            Assert.AreEqual(expected, actual);


        }


        [TestMethod]
        public void LoadIDTest()
        {
            //Tests to if Number of Employees is Equal to Number of Employees in Database


            Employer employer = new Employer();
            employer.EmployerLoadById(Guid.Parse("677e6aab-075b-4329-9025-a99422bd1d77"));


            string expected = "Alex";
            string actual = employer.RepresentativeFirstName;



            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void DeleteTest()
        {

            Employer employer = new Employer();
            employer.EmployerId = Guid.Parse("8368a3ce-7fd2-4ac3-a786-45007c87a817");
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
            employer.EmployerId = Guid.Parse("a9bf5d34-54aa-4f48-9b90-009b2ee15bb7");
            employer.UserName = "Test123!";
            employer.Industry = "TestIndustry";
            employer.EmployerUpdate();


            employer.EmployerLoadById(Guid.Parse("a9bf5d34-54aa-4f48-9b90-009b2ee15bb7"));

            string expected = "Test123!TestIndustry";
            string actual = employer.UserName + employer.Industry;

            Assert.AreEqual(expected, actual);


        }



    }
}
