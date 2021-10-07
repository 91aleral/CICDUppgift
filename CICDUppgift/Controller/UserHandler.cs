using CICDUppgift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDUppgift.Controller
{
    public class UserHandler
    {



        public static void CreateNewUser()
        {
            User User1 = new User()
            {
                ID = 1,
                userName = "Alex",
                password = "Hej1",
                role = "User",
                salary = 400,
                balance = 2000

            };

            string user2 = User1.ToString();


        }

    }
}
