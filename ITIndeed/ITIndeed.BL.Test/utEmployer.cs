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




            employer.UserName = "foxValleyBadgerMan";
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
    }
}
