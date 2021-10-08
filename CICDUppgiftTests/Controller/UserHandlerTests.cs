using Microsoft.VisualStudio.TestTools.UnitTesting;
using CICDUppgift.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CICDUppgift.Controller.Tests
{
    [TestClass()]
    public class UserHandlerTests
    {
        [TestMethod()]
        public void AccessTxtTest()
        {
            StreamReader sr = new StreamReader("..//..//..//..//CICDUppgift//Users.txt");
            var line = sr.ReadLine();
            var expected = line.Contains("Admin");
            var actual = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoginUserTest_1()
        {
            var actual = UserHandler.LoginUser("Admin", "admin123");
            var expected = 1;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoginUserTest_2()
        {
            var actual = UserHandler.LoginUser("hejsan", "duDin");
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetUsersTest_1()
        {
            List<string> Users = File.ReadAllLines(UserHandler.userPath).ToList();

            var expected = Users.Count();
            var actual = UserHandler.GetUsers().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetUsersTest_2()
        {
            var actual = UserHandler.GetUsers()[0].userName;

            Assert.AreEqual("Admin", actual);
        }

        [TestMethod()]
        public void GetUsersTest_3()
        {
            var actual = UserHandler.GetUsers()[0].password;

            Assert.AreEqual("admin123", actual);
        }

        [TestMethod()]
        public void GetUserTest_1()
        {
            var actual = UserHandler.GetUser(5).userName;
            var expected = "Karl";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddNewUserTest_1()
        {
            UserHandler.AddNewUser("test", "test123", "Test", 15, 250, "User");
            var list = UserHandler.GetUsers();
            var expected = list.Last().userName;
            var actual = "test";

            list.RemoveAt(list.Count - 1);
            UserHandler.listOfUser(list);
            Assert.AreEqual(expected, actual);

        }
    }
}