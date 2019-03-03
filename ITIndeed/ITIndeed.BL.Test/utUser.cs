using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITIndeed.BL;
using ITIndeed.PL;
using System.Linq;

namespace ITIndeed.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void InsertTest()
        {
            User user = new User();
            user.UserName = "Dumb Jerk95";
            user.Password = "maple";
            user.Insert();

            ITIndeedEntities dc = new ITIndeedEntities();

            var users = dc.tblUsers;

            int expected = 6;

            int actual = users.Count();

            Assert.AreEqual(expected, actual);

            

           

        }


        [TestMethod]
        public void UpdateTest()
        {
            User user = new User();

            //Make sure NO SPACES when pasting GUIDs from database - taw - 03022019

            user.Id = Guid.Parse("6b444af5-21be-4341-8e67-212630d15549");

            user.UserName = "Dumb Jerk666";
            user.Password = "PassWordUpdateTest17";
            user.Update();

            ITIndeedEntities dc = new ITIndeedEntities();

            User otherUserObject = new User();
            otherUserObject.LoadById(user.Id);

            string expected1 = "Dumb Jerk666";
            string actual1 = otherUserObject.UserName;


            string expected2 = "OiVFS2q2V0c4dd7LcGDl97L6rx0=";
            string actual2 = otherUserObject.Password;


            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);




        }


        [TestMethod]
        public void DeleteTest()
        {
            User user = new User();

            //Make sure NO SPACES when pasting GUIDs from database - taw - 03022019

            user.Id = Guid.Parse("b859717c-8065-43d2-9a5f-4eeb69e6bc6a");

           
            user.Delete();

            ITIndeedEntities dc = new ITIndeedEntities();


            var users = dc.tblUsers;

            int expected = 10;

            int actual = users.Count();

            Assert.AreEqual(expected, actual);




        }



        [TestMethod]
        public void LoadByIdTest()
        {
            User user = new User();

            //Make sure NO SPACES when pasting GUIDs from database - taw - 03022019

            user.LoadById(Guid.Parse("7158cfc3-e8a8-4606-9033-934410ad7c41"));


            string expected = "jess2";
            string actual = user.UserName;


            Assert.AreEqual(expected, actual);


        }


        [TestMethod]
        public void LoadUserNameTest()
        {
            User user = new User();

            //Make sure NO SPACES when pasting GUIDs from database - taw - 03022019

            user.LoadByUserName("jess2");


            string expected = "jess2";
            string actual = user.UserName;


            Assert.AreEqual(expected, actual);


        }



        [TestMethod]
        public void LoadUserList()
        {

            UserList users = new UserList();

            users.Load();


            int expected = 10;
            int actual = users.Count();


            Assert.AreEqual(expected, actual);


        }


        [TestMethod]
        public void LoginTest()
        {

            User user = new User();

            user.UserName = "Dumb Jerk95";
            user.Password = "maple";


            user.Login();

            Assert.AreEqual(user.Login(), true);
        
            


        }

    }
}
