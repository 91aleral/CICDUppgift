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
        public void CheckForUserTest()
        {
            StreamReader sr = new StreamReader("..//..//..//..//CICDUppgift//Users.txt");
            var line = sr.ReadLine();
            var expected = line.Contains("Alex");
            var actual = true;
            Assert.AreEqual(expected, actual);
        }
    }
}