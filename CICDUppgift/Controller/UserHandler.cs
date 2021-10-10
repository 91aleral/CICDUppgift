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
            List<string> Users = File.ReadAllLines(userPath).ToList();
            List<User> userUsers = new List<User>();
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
                if (user.ID == ID)
                {
                    return user;
                }
            }
            return null;
        }

        public static void AddNewUser(string userName, string password, string role, int salary, int balance, string accountType)
        {
            StreamWriter sw = new StreamWriter(userPath, true);
            string user = newID() + ":" + userName + ":" + password + ":" + role + ":" + salary + ":" + balance + ":" + accountType;

            sw.WriteLine(user);
            sw.Close();
        }

        public static void listOfUser(List<User> users)
        {            

            foreach (var item in users)
            {
                using (StreamWriter sw = new StreamWriter(userPath, true))
                {
                    string user = newID() + ":" + item.userName + ":" + item.password + ":" + item.role + ":" + item.salary + ":" + item.balance + ":" + item.accountType;

                sw.WriteLine(user);
                sw.Close();

            }
            }

        }

        public static int newID()
        {
            // List<string> Users = Users = File.ReadAllLines(userPath).ToList();

            List<string> Users = new List<string>();
            using (var stream = new FileStream(userPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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




            var lastID = Users.Last().Split(':');
            //Substring(0, 1);
            int ID = Convert.ToInt32(lastID[0]);

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
