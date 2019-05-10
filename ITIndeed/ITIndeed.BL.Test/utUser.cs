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
            user.UserName = "UserName";
            user.Password = "Password";
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
            user.BaseUserID = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            user.UserName = "UserName";
            user.Password = "Password";
            user.UserUpdate();

            ITIndeedEntities dc = new ITIndeedEntities();

            User otherUserObject = new User();
            otherUserObject.UserLoadById(user.BaseUserID);

            string expected1 = "expected1";
            string actual1 = otherUserObject.UserName;

            string expected2 = "expected2";
            string actual2 = otherUserObject.Password;

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void DeleteTest()
        {
            User user = new User();

            user.BaseUserID = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

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

            user.UserLoadById(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            string expected = "expected";
            string actual = user.UserName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadUserNameTest()
        {
            User user = new User();

            user.UserLoadByUserName("UserName");

            string expected = "UserName";
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

            user.UserName = "UserName";
            user.Password = "Password";

            user.UserLogin();

            Assert.AreEqual(user.UserLogin(), true);
        }

    }
}
