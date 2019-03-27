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


            Byte[] arrBYTE = new Byte[10000];

            employer.UserName = "foxValleyBadgerMan8";
            employer.Password = "maple";
            employer.RepresentativeFirstName= "Bonny4438";
            employer.RepresentativeLastName= "Clyde4438";
            employer.Email = "bonnytheman@bemis.com38";
            employer.Industry = "Retail38";
            employer.OrganizationName = "Bemis Company38";
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
            


            Employer employer = new Employer();
            employer.EmployerLoadById(Guid.Parse("af9411dd-a4bb-4dff-8e07-24c4180caa7e"));


            string expected = "Bonny4438";
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
            employer.EmployerId = Guid.Parse("38ffe608-d859-47b6-a5b7-909301ea1c47");
            employer.UserName = "STUFF1";
            employer.Industry = "STUFF1";
            employer.EmployerUpdate();


            employer.EmployerLoadById(Guid.Parse("38ffe608-d859-47b6-a5b7-909301ea1c47"));

            string expected = "STUFF1STUFF1";
            string actual = employer.UserName + employer.Industry;

            Assert.AreEqual(expected, actual);


        }



    }
}
