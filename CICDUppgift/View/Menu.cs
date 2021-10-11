namespace CICDUppgift.View
{
    using System;
    using System.Collections.Generic;
    using CICDUppgift.Model;
    using CICDUppgift.Controller;
    using System.Threading;

    public class Menu : UserHandler
    {
        public static void MainMenu(User user)
        {
            Console.Clear();
            if (user.accountType == "User")
            {
                Console.WriteLine("Logged in as {0}, role: {1}.  Current balance: {2} ,  monthly salary: {3}\n\n", user.userName, user.role, user.balance, user.salary);
                Console.WriteLine("1. Request change to current role or salary");
                Console.WriteLine("2. Remove user");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RequestChange(user);
                        break;
                    case "2":
                        DeleteCurrentUser(user);
                        break;

                    default:
                        MainMenu(user);
                        break;

                }
            }

            else if (user.accountType == "Admin")
            {
                Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
                Console.WriteLine("1. Show all current users");
                Console.WriteLine("2. Show requested userchanges");
                Console.WriteLine("3. Pay out all salaries");
                Console.WriteLine("4. Create new user");
                Console.WriteLine("5. Remove user");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GetAllUsers(user);
                        break;
                    case "2":
                        showUserRequests(user);
                        break;
                    case "3":
                        PayOutRequest(user);
                        break;
                    case "4":
                        AddUser(user);
                        break;
                    case "5":
                        AdminDeleteUser(user);
                        break;

                    default:
                        MainMenu(user);
                        break;
                }
            }
        }

        public static void RequestChange(User user)
        {
            string request;
            bool success;
            do
            {
                Console.WriteLine("Request change to current role or salary");
                request = Console.ReadLine().ToLower();
                if (request == "role" || request == "salary")
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            } while (!success);

            if (request == "role" || request == "Role")
            {
                Console.WriteLine("Enter a new role: ");
                var RequestedRole = Console.ReadLine();
                Console.WriteLine("Are you sure you want to request to change from {0} to {1}?", user.role, RequestedRole);
                Console.WriteLine("Y/N");
                var input = Console.ReadLine();

                if (input == "Y" || input == "y")
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
            else if (request == "salary" || request == "Salary")
            {
                Console.WriteLine("Enter a new salary: ");
                var RequestedSalary = Console.ReadLine();

                Console.WriteLine("Are you sure you want to request to change from {0} to {1}?", user.salary, RequestedSalary);
                Console.WriteLine("Y/N");
                var input = Console.ReadLine();

                if (input == "Y" || input == "y")
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
            Console.WriteLine("Redirecting to menu...");
            Thread.Sleep(2500);
            MainMenu(user);

        }

        public static void AddUser(User user)
        {
            Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
            Console.WriteLine("Add new user:   (username, password, role, salary, balance, accountType");
            Console.WriteLine("Enter a username:");
            var username = getNumberAndLetterInput();
            Console.WriteLine("Enter a password:");
            var password = getNumberAndLetterInput();
            Console.WriteLine("Enter a role:");
            var role = getLetterInput();
            Console.WriteLine("Enter a salary:");
            int salary = getDigitInput(); ;
            Console.WriteLine("Enter a account type (User/Admin):");
            var accountType = getLetterInput();

            if (accountType != "User" && accountType != "Admin")
            {
                Console.WriteLine("Incorrect data! Returning to menu...");
                Thread.Sleep(2000);
                MainMenu(user);
            }

            Console.WriteLine("\n\nCreate user {0}, password: {1}, role: {2}, salary: {3}, accountType: {5}?", username, password, role, salary, accountType);
            Console.WriteLine("Y/N");
            var input = Console.ReadLine();
            if (input == "Y" || input == "y")
            {
                AddNewUser(username, password, role, salary, 0, accountType);
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
            var userList = GetUsers();

            foreach (var item in userList)
            {
                Console.WriteLine("Username: {0}, password: {1}", item.userName, item.password);
            }

            Console.WriteLine("\n\n Press any key to return to menu");
            Console.ReadKey();
            MainMenu(user);
        }

        public static void DeleteCurrentUser(User user)
        {
            // Hämtar users
            List<User> users = GetUsers();
            Console.WriteLine("Remove current user... \n\n");
            // User input
            Console.WriteLine("Enter your username: ");
            var username = Console.ReadLine();


            // Mer funktionalitet - Are you sure you want to delete me?

            // User input
            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();
            if (username == user.userName && password == user.password)
            {
                Console.WriteLine($"Are you sure you want to remove your user {user.userName}?  Y/N");
                var input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    DeleteUser(username, password);
                    Login();
                }
                else
                {
                    MainMenu(user);
                }
            }
            else
            {
                Console.WriteLine("Incorrect username or password");
                MainMenu(user);
            }
        }

        public static void AdminDeleteUser(User user)
        {
            Console.Clear();
            // Hämtar users
            List<User> users = GetUsers();
            Console.WriteLine("Users: \n\n");
            foreach (var item in users)
            {
                Console.WriteLine($"Username:{item.userName}  Password:{item.password}  Role:{item.role}");
            }

            Console.WriteLine("\nEnter a username to remove:");
            var username = Console.ReadLine();

            Console.WriteLine("Enter the user's password:");
            var password = Console.ReadLine();

            if (users.Exists(u => u.userName == username && u.password == password))
            {
                Console.WriteLine($"Are you sure you want to remove user {username}?  Y/N");
                var input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    DeleteUser(username, password);
                    Console.WriteLine($"User {username} has been removed!");
                    Console.WriteLine("Returning to menu..");
                    Thread.Sleep(2500);
                    MainMenu(user);
                }
                else
                {
                    MainMenu(user);
                }
            }
            else
            {
                Console.WriteLine("Something went wrong.");
                Console.WriteLine("Press any key to continue..");
                Console.Read();
                MainMenu(user);
            }
        }

        public static void showUserRequests(User user)
        {
            var requests = GetAdminLog();
            Console.Clear();
            if (requests.Count <= 1)
            {
                Console.WriteLine("No requests to show! Returning to menu...");
                Thread.Sleep(2000);
                MainMenu(user);
            }

            Console.WriteLine("Current requests: \n");


            for (int i = 1; i < requests.Count; i++)
            {
                Console.WriteLine($" {i}. User {requests[i].userName}, {requests[i].role} requesting updated {requests[i].change} from {requests[i].currentValue} to {requests[i].newValue}");
            }
            Console.WriteLine("Press 1 to perform changes or 2 to remove a change.");
            var performance = Console.ReadLine();
            if (performance == "2")
            {
                Console.WriteLine("Enter the number to remove: ");
                int numberToRemove = getDigitInput();
                if (numberToRemove >= requests.Count || numberToRemove <= 0)
                {
                    Console.WriteLine("Nothing to match...");
                    Thread.Sleep(2000);
                    MainMenu(user);
                }

                requests.RemoveAt(numberToRemove);
                OverwriteRequests(requests);
                MainMenu(user);
            }
            else if ( performance == "1")
            {
                Console.WriteLine("\n Enter the number of a request to execute it (e.g 3)!");
                int indexToChange = getDigitInput();

                if (indexToChange >= requests.Count)
                {
                    Console.WriteLine("Incorrect choice! Returning to menu");
                    Thread.Sleep(2000);
                    MainMenu(user);
                }
                executeRequest(requests[indexToChange]);
                requests.RemoveAt(indexToChange);
                OverwriteRequests(requests);
                MainMenu(user);
            }
            else
            {
                Console.WriteLine("Incorrect choice! Returning to menu");
                Thread.Sleep(2000);
                MainMenu(user);
            }
        }

        public static void PayOutRequest(User user)
        {
            Console.WriteLine("Would you like to pay all users? Y/N");
            var input = Console.ReadLine();

            if (input == "Y" || input == "y")
            {
                MonthlyPayOut();
                Console.WriteLine("Payout has been done!");
                Thread.Sleep(2000);
                MainMenu(user);
            }
            else
            {
                Console.WriteLine("Incorrect choice! Returning to menu...");
                Thread.Sleep(2000);
                MainMenu(user);
            }
        }
    }
}
