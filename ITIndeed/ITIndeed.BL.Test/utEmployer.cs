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




            employer.UserName = "foxValleyBadgerMan2";
            employer.Password = "maple";
            employer.RepresentativeFirstName= "Bonny444";
            employer.RepresentativeLastName= "Clyde444";
            employer.Email = "bonnytheman@bemis.com";
            employer.Industry = "Retail";
            employer.OrganizationName = "Bemis Company";
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
    }
}
