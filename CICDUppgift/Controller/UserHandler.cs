namespace CICDUppgift.Controller
{
    using CICDUppgift.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Threading;
    using CICDUppgift.View;

    public static class UserHandler
    {
        public static string userPath = "..//..//..//..//CICDUppgift//Users.txt";
        public static string RemovedUserPath = "..//..//..//..//CICDUppgift//RemovedUsers.txt";


        public static User LoginUser(string username, string password)
        {
            List<User> Users = GetUsers();

            foreach (var user in Users)
            {

                if (user.userName == username && user.password == password)
                {
                    Console.Clear();
                    if (user.accountType == "User")
                    {
                        
                        Console.WriteLine($"User {username} logged in");
                        Thread.Sleep(2000);
                        Menu.MainMenu(user);

                        
                    }

                    else if (user.accountType == "Admin")
                    {
                        
                        Console.WriteLine($"Admin {username} logged in");
                        Thread.Sleep(2000);
                        Menu.MainMenu(user);
                    }

                    return user;
                }
            }
            Console.WriteLine("Fail");
            Console.Clear();
            return null;
        }


        public static void Login()
        {
            User user;
            do
            {
                Console.WriteLine("Enter username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                var password = Console.ReadLine();

                user = LoginUser(username, password);
            } while (user == null);

           

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

        // Fungerar inte. Skriver över ALLT i txt filen..
        public static void DeleteUser(User user)
        {
            // Hämtar users
            List<User> users = GetUsers();

            // temp fil av userPath

            // User input
            Console.WriteLine("Enter your username: ");
            var username = Console.ReadLine();

            // User input
            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();

            if (username == user.userName && password == user.password)
            {
                users.RemoveAll(u => u.userName == username && u.password == password);
                UserHandler.OverwritelistOfUser(users);
            }
            else
            {
                Console.WriteLine("Incorrect username or password");
            }
            


        }

        public static void AddNewUser(string userName, string password, string role, int salary, int balance, string accountType)
        {
            StreamWriter sw = new StreamWriter(userPath, true);
            string user = newID() + ":" + userName + ":" + password + ":" + role + ":" + salary + ":" + balance + ":" + accountType;

            sw.WriteLine(user);
            sw.Close();
        }

        /// <summary>
        /// Skriver användare till txt
        /// </summary>
        /// <param name="users"></param>
        public static void listOfUser(List<User> users)
        {


            foreach (var item in users)
            {
                using (StreamWriter sw = new StreamWriter(userPath, true))
                {
                    string user = newID() + ":" + item.userName + ":" + item.password + ":" + item.role + ":" + item.salary + ":" + item.balance + ":" + item.accountType;

                    sw.WriteLine(user);
                    sw.Close();
                };

            }

        }

        public static void OverwritelistOfUser(List<User> users)
        {
            newTxtList();
            users.RemoveAt(0);
            foreach (var item in users)
            {
                using (StreamWriter sw = new StreamWriter(userPath, true))
                {
                    string user = newID() + ":" + item.userName + ":" + item.password + ":" + item.role + ":" + item.salary + ":" + item.balance + ":" + item.accountType;

                    sw.WriteLine(user);
                    sw.Close();
                };

            }

        }

        public static void newTxtList()
        {

            using (StreamWriter sw = new StreamWriter(userPath, false))
            {
                string user = 1 + ":" + "Admin" + ":" + "admin123" + ":" + "User" + ":" + 0 + ":" + 0 + ":" + "Admin";
                sw.WriteLine(user);
                sw.Close();
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
