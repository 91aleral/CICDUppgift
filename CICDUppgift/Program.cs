using CICDUppgift.Model;
using System;
using System.IO;

namespace CICDUppgift
{
    class Program
    {
        static void Main(string[] args)
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

            string user2 = User1.ID + ":" + User1.userName + ":" + User1.password + ":" + User1.role + ":" + User1.salary + ":" + User1.balance;
            StreamWriter sw = new StreamWriter("../../../Users.txt");
            sw.WriteLine(user2);
            Console.WriteLine(user2);
            sw.Close();

            Console.Read();



             
            


   

        }
    }
}
