namespace CICDUppgift.View
{
    using System;
    using System.Collections.Generic;
    using CICDUppgift.Model;
    using CICDUppgift.Controller;
    using System.Threading;

    public class Menu : UserHandler
    {
        /// <summary>
        /// Huvudmenyn för antingen en User eller en Admin, med dess olika val att göra.
        /// En check görs om det är en Admin eller en User
        /// </summary>
        /// <param name="user">Användare</param>
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
                        ShowUserRequests(user);
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

        /// <summary>
        /// En meny för användaren för att begära en förändring på sitt konto.
        /// Ex: Ändra sin Roll eller Lön.
        /// </summary>
        /// <param name="user">Användare</param>
        public static void RequestChange(User user)
        {
            Console.Clear();
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
                var RequestedRole = GetLetterInput();
                Console.WriteLine("Are you sure you want to request to change from {0} to {1}?", user.role, RequestedRole);
                Console.WriteLine("Y/N");
                var input = Console.ReadLine();

                if (input == "Y" || input == "y")
                {
                    SaveChangeRequest(user.userName, user.role, request, user.role, RequestedRole);
                    Console.Clear();
                    Console.WriteLine($"Request added {RequestedRole} from {user.role}\n\n");
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Cancelling, Redirecting to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }
            }
            else if (request == "salary" || request == "Salary")
            {
                Console.WriteLine("Enter a new salary: ");
                var RequestedSalary = GetDigitInput();

                Console.WriteLine("Are you sure you want to request to change from {0} to {1}?", user.salary, RequestedSalary);
                Console.WriteLine("Y/N");
                var input = Console.ReadLine();

                if (input == "Y" || input == "y")
                {
                    Console.Clear();
                    SaveChangeRequest(user.userName, user.role, request, user.salary.ToString(), RequestedSalary.ToString()); ;
                    Console.WriteLine($"Request added {RequestedSalary} from {user.salary}\n\n");
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(1500);
                    MainMenu(user);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Cancelling, Redirecting to menu...");
                    Thread.Sleep(2500);
                    MainMenu(user);
                }
            }
        }

        /// <summary>
        /// Login meny - Det användare anger skickas vidare till LoginUser metoden
        /// som sedan kollar om användare finns eller ej och hämtar dess data.
        /// </summary>
        public static void Login()
        {
            User user;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                var password = Console.ReadLine();               

                user = LoginUser(username, password);

                if (user == null)
                {
                    Console.WriteLine("Incorrect username or password!");
                    Thread.Sleep(2000);
                }
            } while (user == null);

            Console.Clear();
            Console.WriteLine($"Logging in as {user.role} {user.userName}\n\n");
            Console.WriteLine("Redirecting to menu...");
            Thread.Sleep(2500);
            MainMenu(user);
        }

        /// <summary>
        /// Meny för att lägga till en ny användare.
        /// Gör checkar om användare finns.
        /// 
        /// Admin skapar användaren och skriver in dess info som tillhör användaren.
        /// </summary>
        /// <param name="user">Användare</param>
        public static void AddUser(User user)
        {
            Console.Clear();
            Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
            Console.WriteLine("Add new user:   (username, password, role, salary, balance, accountType");
            bool userExists;
            string username;
            do
            {
            Console.WriteLine("Enter a username:");
            username = GetNumberAndLetterInput();
            userExists = CheckIfUsernameExists(username);
                if (userExists)
                {
                    Console.WriteLine("Username already in use!");
                }

            } while (userExists);
           
            Console.WriteLine("Enter a password:");
            var password = GetNumberAndLetterInput();
            Console.WriteLine("Enter a role:");
            var role = GetLetterInput();
            Console.WriteLine("Enter a salary:");
            int salary = GetDigitInput(); ;
            Console.WriteLine("Enter a account type (User/Admin):");
            var accountType = GetLetterInput();

            if (accountType != "User" && accountType != "Admin")
            {
                Console.WriteLine("Incorrect data! Returning to menu...");
                Thread.Sleep(2000);
                MainMenu(user);
            }

            Console.WriteLine("\n\nCreate user {0}, password: {1}, role: {2}, salary: {3}, accountType: {4}?", username, password, role, salary, accountType);
            Console.WriteLine("Y/N");
            var input = Console.ReadLine();
            if (input == "Y" || input == "y")
            {
                AddNewUser(username, password, role, salary, accountType);
                MainMenu(user);
            }
            else
            {
                Console.WriteLine("Cancelled.");
                Thread.Sleep(2000);
                MainMenu(user);
            }
        }

        /// <summary>
        /// Meny - hämtar alla användare till Admin.
        /// Man ser: Username, Role och password.
        /// </summary>
        /// <param name="user">Användare</param>
        public static void GetAllUsers(User user)
        {
            Console.Clear();
            Console.WriteLine("Logged in as {0}, role: {1}\n\n", user.userName, user.role);
            var userList = GetAllUsersToList();

            foreach (var item in userList)
            {
                Console.WriteLine("Username: {0}\nRole: {1}\nPassword: {2}", item.userName,item.role, item.password);
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            }

            Console.WriteLine("\n\n Press any key to return to menu");
            Console.ReadKey();
            MainMenu(user);
        }

        /// <summary>
        /// Meny för användare - raddera sitt egna konto ifrån systemet.
        /// </summary>
        /// <param name="user">Användare</param>
        public static void DeleteCurrentUser(User user)
        {
            List<User> users = GetAllUsersToList();
            Console.Clear();
            Console.WriteLine("Remove current user... \n\n");

            Console.WriteLine("Enter your username: ");
            var username = Console.ReadLine();

            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();
            if (username == user.userName && password == user.password)
            {
                Console.Clear();
                Console.WriteLine($"Are you sure you want to remove your user {user.userName}?  Y/N");
                var input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    Console.Clear();
                    Console.WriteLine($"Removing user {user.userName}, you will now be logged out. ");
                    Thread.Sleep(3000);
                    DeleteUser(username, password);
                    Login();
                }
                else
                {
                    Console.WriteLine("Cancelling, Redirecting to menu..");
                    Thread.Sleep(3000);
                    MainMenu(user);
                }
            }
            else
            {
                Console.WriteLine("Incorrect username or password!");
                Console.WriteLine("Redirecting to menu..");
                Thread.Sleep(2500);
                MainMenu(user);
            }
        }

        /// <summary>
        /// Meny för admin - raddera ett konto/användare ifrån systemet.
        /// </summary>
        /// <param name="user"></param>
        public static void AdminDeleteUser(User user)
        {
            Console.Clear();
            
            List<User> users = GetAllUsersToList();
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
                Console.WriteLine("Returning to menu..");
                Thread.Sleep(2500);
                MainMenu(user);
            }
        }

        /// <summary>
        /// Meny för admin - Visar information om en användare har begärt ändring på sitt konto.
        /// Ex: Roll eller Lön.
        /// Admin kan sedan godkänna eller neka förändringen och datan uppdateras.
        /// </summary>
        /// <param name="user">Användare</param>
        public static void ShowUserRequests(User user)
        {
            var requests = GetAdminLog();
            Console.Clear();
            if (requests.Count <= 1)
            {
                Console.WriteLine("No requests to show! Redirecting to menu...");
                Thread.Sleep(2000);
                MainMenu(user);
            }

            Console.WriteLine("Current requests: \n");

            for (int i = 1; i < requests.Count; i++)
            {
                Console.WriteLine($"{i}. User {requests[i].userName}, {requests[i].role} requesting updated {requests[i].change} from {requests[i].currentValue} to {requests[i].newValue}");
            }

            Console.WriteLine("\nPress 1 to implement a request or 2 to remove a request:");
            var performance = Console.ReadLine();

            if (performance == "2")
            {
                Console.WriteLine("Enter the number to remove: ");
                int numberToRemove = GetDigitInput();
                if (numberToRemove >= requests.Count || numberToRemove <= 0)
                {
                    Console.WriteLine("Nothing to match...");
                    Thread.Sleep(2000);
                    MainMenu(user);
                }

                requests.RemoveAt(numberToRemove);
                OverwriteSavedChangeRequests(requests);
                MainMenu(user);
            }
            else if ( performance == "1")
            {
                Console.WriteLine("\n Enter the number of a request to execute it (e.g 3)!");
                int indexToChange = GetDigitInput();

                if (indexToChange >= requests.Count)
                {
                    Console.WriteLine("Incorrect choice! Redirecting to menu");
                    Thread.Sleep(2500);
                    MainMenu(user);
                }

                ExecuteChangeRequest(requests[indexToChange]);
                requests.RemoveAt(indexToChange);
                OverwriteSavedChangeRequests(requests);
                MainMenu(user);
            }
            else
            {
                Console.WriteLine("Incorrect choice! Redirecting to menu");
                Thread.Sleep(2500);
                MainMenu(user);
            }
        }

        /// <summary>
        /// Meny för admin - Valmöjlighet om admin vill betala ut månadslön eller inte till alla användare.
        /// </summary>
        /// <param name="user">Användare</param>
        public static void PayOutRequest(User user)
        {
            Console.Clear();
            Console.WriteLine("Would you like to pay all users? Y/N");
            var input = Console.ReadLine();

            if (input == "Y" || input == "y")
            {
                var money = PayOutAllSalaries();
                Console.Clear();
                Console.WriteLine($"A total of {money}$ payout has been done!");
                Console.WriteLine("\nRedirecting to menu...");
                Thread.Sleep(3500);

                MainMenu(user);
            }
            else if(input == "N" || input == "n")
            {
                Console.WriteLine("Redirecting to menu...");
                Thread.Sleep(2500);
                MainMenu(user);
            }
            else
            {
                Console.WriteLine("Incorrect choice! Redirecting to menu...");
                Thread.Sleep(2500);
                MainMenu(user);
            }
        }
    }
}