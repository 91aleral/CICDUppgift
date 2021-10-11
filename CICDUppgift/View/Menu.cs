using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CICDUppgift.Model;
using CICDUppgift.Controller;
using System.Threading;

namespace CICDUppgift.View
{
    public class Menu : UserHandler
    {
        public static void MainMenu(User user)
        {
            Console.Clear();
            if (user.accountType == "User")
            {
                Console.WriteLine("Logged in as {0}, role: {1}.  Current balance: {2} ,  monthly salary: {3}\n\n", user.userName, user.role, user.balance, user.salary);
                Console.WriteLine("1. Request change to current role or salary");
                Console.WriteLine("2. Change password");
                Console.WriteLine("3. Remove user");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RequestChange(user);
                        break;
                    case "2":
                        Console.WriteLine("piss off");
                        break;
                    case "3":

                        break;

                    default:
                        MainMenu(user);
                        break;

                }
            }
            else if (user.accountType == "Admin")
            {
                Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
                Console.WriteLine("1. Change password");
                Console.WriteLine("2. Show all current users");
                Console.WriteLine("3. Show requested userchanges");
                Console.WriteLine("4. Pay out all salaries");
                Console.WriteLine("5. Create new user");
                Console.WriteLine("6. Remove user");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("wwww");
                        break;
                    case "2":
                        GetAllUsers(user);
                        break;
                    case "3":
                        Console.WriteLine("w");
                        break;
                    case "4":
                        Console.WriteLine("ww");
                        break;
                    case "5":
                        AddUser(user);
                        break;
                    case "6":
                        Console.WriteLine("www");
                        break;

                    default:
                        MainMenu(user);
                        break;
                }
            }





        }

        public static void RequestChange(User user)
        {
            //    string userName, string userRole, string typeOfChange, string currentValue, string newValue
            string request;
            bool success;
            do
            {
                Console.WriteLine("Request change to current role or salary");
                request = Console.ReadLine();
                if (request == "role" || request == "salary")
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            } while (!success);

            if (request == "role")
            {
                Console.WriteLine("Enter a new role: ");
                var RequestedRole = Console.ReadLine();
                Console.WriteLine("Are you sure you want to request to change {0} to {1}?", user.role, RequestedRole);
                Console.WriteLine("Y/N");
                var input = Console.ReadLine();

                if (input == "Y")
                {
                    ChangeRequest(user.userName, user.role, request, user.role, RequestedRole);
                    Console.WriteLine($"Request added {RequestedRole} from {user.role}\n\n");
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }
                else
                {
                    Console.WriteLine("Cancelling, getting redirected to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }



            }
            else if (request == "salary")
            {
                Console.WriteLine("Enter a new salary: ");
                var RequestedSalary = Console.ReadLine();

                Console.WriteLine("Are you sure you want to request to change {0} to {1}?", user.salary, RequestedSalary);
                Console.WriteLine("Y/N");
                var input = Console.ReadLine();

                if (input == "Y")
                {
                    ChangeRequest(user.userName, user.role, request, user.salary.ToString(), RequestedSalary);
                    Console.WriteLine($"Request added {RequestedSalary} from {user.salary}\n\n");
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }
                else
                {
                    Console.WriteLine("Cancelling, getting redirected to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }
            }
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

            Console.WriteLine($"Logging in as {user.role} {user.userName}\n\n");
            Console.WriteLine("Getting redirected to menu...");
            Thread.Sleep(1500);
            MainMenu(user);

        }

        public static void AddUser(User user)
        {
            Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
            Console.WriteLine("Add new user:   (username, password, role, salary, balance, accountType");
            Console.WriteLine("Enter a username:");
            var username = UserHandler.getNumberAndLetterInput();
            Console.WriteLine("Enter a password:");
            var password = UserHandler.getNumberAndLetterInput();
            Console.WriteLine("Enter a role:");
            var role = UserHandler.getLetterInput();
            Console.WriteLine("Enter a salary:");
            int salary = UserHandler.getDigitInput();
            Console.WriteLine("Enter a balance:");
            int balance = UserHandler.getDigitInput();
            Console.WriteLine("Enter a role:");
            var accountType = UserHandler.getLetterInput();



            Console.WriteLine("\n\nCreate user {0}, password: {1}, role: {2}, salary: {3}, balance: {4}, accountType: {5}?", username, password, role, salary, balance, accountType);
            Console.WriteLine("Y/N");
            var input = Console.ReadLine();
            if (input == "Y")
            {
                UserHandler.AddNewUser(username, password, role, salary, balance, accountType);
                MainMenu(user);
            }
            else
            {
                Console.WriteLine("Cancelled.");
                Thread.Sleep(2000);
                MainMenu(user);

            }
        }

        public static void GetAllUsers(User user)
        {
            Console.Clear();
            Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
            var userList = UserHandler.GetUsers();

            foreach (var item in userList)
            {
                Console.WriteLine("Username: {0}, password: {1}", item.userName, item.password);
            }

            Console.WriteLine("\n\n Press any key to return to menu");
            Console.ReadKey();
            MainMenu(user);
        }

        public static void DeleteSelectedUser(User user)
        {
            // Hämtar users
            List<User> users = UserHandler.GetUsers();

            // User input
            Console.WriteLine("Enter your username: ");
            var username = Console.ReadLine();


            // Mer funktionalitet - Are you sure you want to delete me?

            // User input
            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();

            if (username == user.userName && password == user.password)
            {
                UserHandler.DeleteUser(username, password);
            }
            else
            {
                Console.WriteLine("Incorrect username or password");
            }
        }
    }
}
