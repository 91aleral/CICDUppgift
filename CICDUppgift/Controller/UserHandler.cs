using CICDUppgift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CICDUppgift.Controller
{
    public static class UserHandler
    {
       public static string userPath = "..//..//..//..//CICDUppgift//Users.txt";


        public static int LoginUser(string username, string password)
        {

            List<User> Users = GetUsers();

            foreach (var user in Users)
            {             

                if (user.userName == username && user.password == password)
                {                    
                    Console.WriteLine("Success!");
                    
                    return user.ID;
                }
            }
            Console.WriteLine("Fail");
            return 0;
        }


        public static User Login()
        {
            int userID;
            do
            {
                Console.WriteLine("Enter username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                var password = Console.ReadLine();

                userID = LoginUser(username, password);
            } while (userID == 0);

            return GetUser(userID);

        }

        public static List<User> GetUsers()
        {
            List < User > userUsers = new List<User>();
            List<string> Users = File.ReadAllLines(userPath).ToList();
            foreach (var item in Users)
            {
                string[] user = item.Split(':');

                User currentUser = new User
                {
                    ID = Convert.ToInt32(user[0]),
                    userName = user[1],
                    password = user[2],
                    role = user[3],
                    salary = Convert.ToInt32(user[4]),
                    balance = Convert.ToInt32(user[5]),
                    accountType = user[6]
                };

                userUsers.Add(currentUser);
            }

            return userUsers;
        }
        public static User GetUser(int ID)
        {
            List<User> users = GetUsers();

            foreach (var user in users)
            {
                if(user.ID == ID)
                {
                    return user;
                }
            }
            return null;

        }

        public static void AddNewUser(string userName, string password, string role, int salary, int balance) { 
          
            string user = newID() + ":" + userName + ":" + password + ":" + role + ":" + salary + ":" + balance;
            StreamWriter sw = new StreamWriter("../../../Users.txt", true);
            sw.WriteLine(user);
            sw.Close();
        }

        public static int newID() {

            List<string> Users = File.ReadAllLines(userPath).ToList();     
            var lastID = Users.Last().Substring(0, 1);
            int ID = Convert.ToInt32(lastID);
            return ID + 1;           

        }

        public static int getDigitInput()
        {

            int userInput;
            bool parsed = false;
            do
            {
               parsed = Int32.TryParse(Console.ReadLine(), out userInput);
               
                if (!parsed)
                {
                    Console.WriteLine("Incorrect input");
                }

            }
            while (!parsed);

            return userInput;

        }

        public static string getLetterInput()
        {
            string userInput;
            bool result = false;
            do
            {
             userInput = Console.ReadLine();
             result = userInput.All(Char.IsLetter);
                if (!result)
                {
                    Console.WriteLine("Incorrect Input!");
                }

            }
            while (!result);

            return userInput;
        }

        public static string getNumberAndLetterInput()
        {
            string userInput;
            bool result = false;
            do
            {
                userInput = Console.ReadLine();
                result = userInput.All(Char.IsLetterOrDigit);
                if (!result)
                {
                    Console.WriteLine("Incorrect Input!");
                }

            }
            while (!result);

            return userInput;
        }
    }
}
