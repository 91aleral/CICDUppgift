namespace CICDUppgift.Controller.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CICDUppgift.Controller;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    [TestClass()]
    public class UserHandlerTests
    {
        /// <summary>
        /// Kollar så man kommer åt Users.txt filen.
        /// </summary>
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

        /// <summary>
        /// Testar så att Admin kan logga in.
        /// </summary>
        [TestMethod()]
        public void LoginUserTest_1()
        {
            var actual = UserHandler.LoginUser("admin1", "admin1234").ID;
            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testar om en användare finns.
        /// </summary>
        [TestMethod()]
        public void NewUserExists()
        {
            var actual = UserHandler.CheckIfUsernameExists("admin1");
            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Laddar in alla users med GetAllUsers metoden.
        /// Jämför antalet användare med användare i text filen.
        /// </summary>
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
            var actual = UserHandler.GetAllUsersToList().Count;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testar att hämta en user på första position i filen.
        /// </summary>
        [TestMethod()]
        public void GetUsersTest_2()
        {
            var actual = UserHandler.GetAllUsersToList()[0].userName;

            Assert.AreEqual("admin1", actual);
        }

        /// <summary>
        /// Testar att lägga till en användare som hamnar sist i filen och sedan tar bort användaren.
        /// </summary>
        [TestMethod()]
        public void AddNewUserAndDeleteTest_1()
        {
            UserHandler.AddNewUser("test", "test123", "Test", 15, "User");
            var list = UserHandler.GetAllUsersToList();
            var expected = list.Last().userName;
            var actual = "test";
            UserHandler.DeleteUser("test", "test123");

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Integration Test 1:
        /// 
        /// Kollar så man kan lägga till en ny användare,
        /// Testar sedan att ta bort användaren man gjorde
        /// och hämtar alla användare
        /// </summary>
        [TestMethod()]
        public void CreateLoginRemoveUserIntegrationTest()
        {
            UserHandler.AddNewUser("test", "test123", "Test", 15, "User");
            var expeted = UserHandler.LoginUser("test", "test123").ID;
            UserHandler.DeleteUser("test", "test123");
            var list = UserHandler.GetAllUsersToList();
            var actual = list.Last().ID;

            Assert.AreNotEqual(expeted, actual);
        }

        /// <summary>
        /// Integration Test 2:
        ///
        /// Kollar så man kan lägga till en ny användare,
        /// Hämtar sedan alla användare,
        /// Testar att logga in med den nya användaren.
        /// Sist tar vi bort användaren vi skapade.
        /// </summary>
        [TestMethod()]
        public void CreateLoginRemoveUserIntegrationTest2()
        {
            UserHandler.AddNewUser("test93", "test123", "Test", 15, "User");
            var list = UserHandler.GetAllUsersToList();
            var expeted = UserHandler.LoginUser("test93", "test123").ID;
            var actual = list.Last().ID;

            Assert.AreEqual(expeted, actual);

            UserHandler.DeleteUser("test93", "test123");
        }

        /// <summary>
        /// Hämtar alla användare från filen.
        /// Testar sedan att betala ut en månadslön till alla användare i filen.
        /// </summary>
        [TestMethod()]
        public void PayoutSalariesTest()
        {
            int balancesBefore = 0;
            int salariesCombined = 0;
            var userList = UserHandler.GetAllUsersToList();

            foreach (var user in userList)
            {
                balancesBefore += user.balance;
                salariesCombined += user.salary;
            }

            int totalbalance = balancesBefore + salariesCombined;

            UserHandler.PayOutAllSalaries();

            var userListAfterPayout = UserHandler.GetAllUsersToList();

            int balancesAfterPayout = 0;
            foreach (var user in userListAfterPayout)
            {
                balancesAfterPayout += user.balance;
            }

            Assert.AreEqual(totalbalance, balancesAfterPayout);
        }
    }
}