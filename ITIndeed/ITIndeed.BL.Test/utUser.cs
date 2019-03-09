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

            //UserName cannot be the same for two Users//Business Rule
            User user = new User();
            user.UserName = "DickyBoy1757";
            user.Password = "maple";
            user.UserInsert();

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

            user.BaseUserID = Guid.Parse("6b444af5-21be-4341-8e67-212630d15549");

            user.UserName = "Dumb Jerk666";
            user.Password = "PassWordUpdateTest17";
            user.UserUpdate();

            ITIndeedEntities dc = new ITIndeedEntities();

            User otherUserObject = new User();
            otherUserObject.UserLoadById(user.BaseUserID);

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

            user.BaseUserID = Guid.Parse("b859717c-8065-43d2-9a5f-4eeb69e6bc6a");

           
            user.UserDelete();

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

            user.UserLoadById(Guid.Parse("7158cfc3-e8a8-4606-9033-934410ad7c41"));


            string expected = "jess2";
            string actual = user.UserName;


            Assert.AreEqual(expected, actual);


        }


        [TestMethod]
        public void LoadUserNameTest()
        {
            User user = new User();

            //Make sure NO SPACES when pasting GUIDs from database - taw - 03022019

            user.UserLoadByUserName("jess2");


            string expected = "jess2";
            string actual = user.UserName;


            Assert.AreEqual(expected, actual);


        }



        [TestMethod]
        public void LoadUserList()
        {

            UserList users = new UserList();

            users.UserListLoad();


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


            user.UserLogin();

            Assert.AreEqual(user.UserLogin(), true);
        
            


        }

    }
}
