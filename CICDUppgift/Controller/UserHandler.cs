namespace CICDUppgift.Controller
{
    using CICDUppgift.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    public static class UserHandler
    {
        public static string userPath = "..//..//..//..//CICDUppgift//Users.txt";
        public static string RemovedUserPath = "..//..//..//..//CICDUppgift//RemovedUsers.txt";


        public static int LoginUser(string username, string password)
        {
            List<User> Users = GetUsers();

            foreach (var user in Users)
            {

                if (user.userName == username && user.password == password)
                {
                    if (user.accountType == "User")
                    {
                        Console.WriteLine($"User {username} logged in");
                    }

                    else if (user.accountType == "Admin")
                    {
                        Console.WriteLine($"Admin {username} logged in");
                    }

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

        // Fungerar inte. Skriver över ALLT i txt filen..
        public static void DeleteUser()
        {
            // Hämtar users
            List<User> users = GetUsers();

            // temp fil av userPath
            string tempFile = Path.GetTempFileName();
            
            // User input
            Console.WriteLine("Enter your username: ");
            var usernameDelete = Console.ReadLine();

            // User input
            Console.WriteLine("Enter your password: ");
            var userPasswordDelete = Console.ReadLine();

            // Går igenom users lista
            foreach (var user in users)
            {
                // Om username == user input && userpassword == user input
                if (user.userName == usernameDelete && user.password == userPasswordDelete)
                {

                    // Läs nuvarande userPath
                    using (var sr = new StreamReader(userPath))
                        // Temporär fil som skriver över userPath
                    using (var sw = new StreamWriter(tempFile))
                    {
                        // Medans user input = Avläsning i userPath och det inte är = null, fortsätt
                        while ((usernameDelete = sr.ReadLine()) != null)
                        {
                            // Om user input inte är = username
                            if (usernameDelete != user.userName)
                            {
                                // Blir user input = username
                                // Skriv över med tom sträng.
                                sw.WriteLine("");

                            }

                        }
                        // Stäng usings
                        sw.Close();
                        sr.Close();
                    }

                    // Radera nuvarande userPath
                    File.Delete(userPath);
                    // Skriv över userPath med temp filen.
                    File.Move(tempFile, userPath);
                }
            }
        }

        public static void AddNewUser(string userName, string password, string role, int salary, int balance, string accountType)
        {
            StreamWriter sw = new StreamWriter(userPath, true);
            string user = newID() + ":" + userName + ":" + password + ":" + role + ":" + salary + ":" + balance + ":" + accountType;

            sw.WriteLine(user);
            sw.Close();
        }

        //public static void listOfUser(List<User> users)
        //{

        //    StreamWriter sw = new StreamWriter(userPath, true);

        //    foreach (var item in users)
        //    {
        //        string user = newID() + ":" + item.userName + ":" + item.password + ":" + item.role + ":" + item.salary + ":" + item.balance + ":" + item.accountType;

        //        sw.WriteLine(user);

        //    }
        //    sw.Close();

        //}

        public static int newID()
        {
            List<string> Users = Users = File.ReadAllLines(userPath).ToList();
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
