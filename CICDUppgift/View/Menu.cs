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
    class Menu
    {



        public static void MainMenu(User user)
        {
            Console.Clear();
            if(user.accountType == "User")
            {
                Console.WriteLine("Logged in as {0}, role: {1}.  Current balance: {2} ,  monthly salary: {3}\n\n", user.userName, user.role, user.balance, user.salary);
                Console.WriteLine("Request change to current role or salary");
                Console.WriteLine("Change password");
                Console.WriteLine("Remove user");
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

            }

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("ww");
                    break;
                case "2":
                    GetAllUsers(user);
                    break;
                case "3":
                    Console.WriteLine("ww");
                    break;
                case "4":
                    Console.WriteLine("ww");
                    break;
                case "5":
                    AddUser(user);
                    break;
                case "6":
                    Console.WriteLine("ww");
                    break;

                default:
                    break;
            }




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
    }
}
