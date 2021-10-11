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
            string line;

            using (StreamReader sr = new StreamReader("..//..//..//..//CICDUppgift//Users.txt"))
            {
                line = sr.ReadLine();
            }
            var expected = line.Contains("admin1");
            var actual = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoginUserTest_1()
        {
            var actual = UserHandler.LoginUser("admin1", "admin1234").ID;
            var expected = 1;
            Assert.AreEqual(expected, actual);
        }



        [TestMethod()]
        public void GetUsersTest_1()
        {
            List<string> Users = new List<string>();

            using (var stream = new FileStream(UserHandler.userPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Users.Add(line);
                    }
                }
            }


            var expected = Users.Count();
            var actual = UserHandler.GetUsers().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetUsersTest_2()
        {
            var actual = UserHandler.GetUsers()[0].userName;

            Assert.AreEqual("admin1", actual);
        }

        [TestMethod()]
        public void AddNewUserAndDeleteTest_1()
        {
            UserHandler.AddNewUser("test", "test123", "Test", 15, 250, "User");
            var list = UserHandler.GetUsers();
            var expected = list.Last().userName;
            var actual = "test";

            UserHandler.DeleteUser("test", "test123");
            Assert.AreEqual(expected, actual);
        }
    }
}