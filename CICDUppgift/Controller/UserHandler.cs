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

    public class UserHandler
    {
        public static string userPath = "..//..//..//..//CICDUppgift//Users.txt";
        public static string AdminLog = "..//..//..//..//CICDUppgift//AdminLog.txt";

        public static User LoginUser(string username, string password)
        {
            List<User> Users = GetAllUsersToList();

            foreach (var user in Users)
            {

                if (user.userName == username && user.password == password)
                {

                    return user;
                }
            }
            return null;
        }


        public static void SaveChangeRequest(string userName, string userRole, string typeOfChange, string currentValue, string newValue)
        {
            string request = userName + ":" + userRole + ":" + typeOfChange + ":" + currentValue + ":" + newValue;

            using (StreamWriter sw = new StreamWriter(AdminLog, true))
            {
                sw.WriteLine(request);
            }
        }


        public static List<User> GetAllUsersToList()
        {
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

        //public static User GetUser(int ID)
        //{
        //    List<User> users = GetAllUsersToList();

        //    foreach (var user in users)
        //    {
        //        if (user.ID == ID)
        //        {
        //            return user;
        //        }
        //    }
        //    return null;
        //}

        public static bool CheckIfUsernameExists(string username)
        {
            List<User> users = GetAllUsersToList();

            foreach (var user in users)
            {
                if (user.userName.ToLower() == username.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static void DeleteUser(string username, string password)
        {
            // Hämtar users
            List<User> users = GetAllUsersToList();

            users.RemoveAll(u => u.userName == username && u.password == password);
            OverwriteSavedUsers(users);




        }

        public static void AddNewUser(string userName, string password, string role, int salary, string accountType)
        {

            string user = GetNewUserID() + ":" + userName + ":" + password + ":" + role + ":" + salary + ":" + 0 + ":" + accountType;
            
            using (StreamWriter sw = new StreamWriter(userPath, true))
            {
                sw.WriteLine(user);
                //sw.Close();
            }
        }

        /// <summary>
        /// Skriver användare till txt
        /// </summary>
        /// <param name="users"></param>
        //public static void listOfUser(List<User> users)
        //{


        //    foreach (var item in users)
        //    {
        //        using (StreamWriter sw = new StreamWriter(userPath, true))
        //        {
        //            string user = newID() + ":" + item.userName + ":" + item.password + ":" + item.role + ":" + item.salary + ":" + item.balance + ":" + item.accountType;

        //            sw.WriteLine(user);
        //            sw.Close();
        //        };

        //    }

        //}

        public static void OverwriteSavedUsers(List<User> users)
        {
            NewUserTxt();
            users.RemoveAt(0);
            foreach (var item in users)
            {
                using (StreamWriter sw = new StreamWriter(userPath, true))
                {
                    string user = GetNewUserID() + ":" + item.userName + ":" + item.password + ":" + item.role + ":" + item.salary + ":" + item.balance + ":" + item.accountType;

                    sw.WriteLine(user);
                    sw.Close();
                };

            }

        }

        public static void NewUserTxt()
        {

            using (StreamWriter sw = new StreamWriter(userPath, false))
            {
                string user = 1 + ":" + "admin1" + ":" + "admin1234" + ":" + "Admin" + ":" + 0 + ":" + 0 + ":" + "Admin";
                sw.WriteLine(user);
                sw.Close();
            }



        }

        public static int GetNewUserID()
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

        public static int GetDigitInput()
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

        public static string GetLetterInput()
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

        public static string GetNumberAndLetterInput()
        {
            string userInput;
            bool result = false;
            bool containsDigit;
            bool containsLetter;
            do
            {
                userInput = Console.ReadLine();
                result = userInput.All(Char.IsLetterOrDigit);
                containsDigit = userInput.Any(Char.IsDigit);
                containsLetter = userInput.Any(Char.IsLetter);
                if (!result || !containsDigit || !containsLetter)
                {
                    Console.WriteLine("Incorrect Input!");
                }
            }
            while (!result || !containsDigit || !containsLetter);

            return userInput;
        }

        public static List<UserRequest> GetAdminLog()
        {
            List<string> requests = new List<string>();

            using (var stream = new FileStream(AdminLog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        requests.Add(line);
                    }
                }
            }

            List<UserRequest> userRequests= new List<UserRequest>();
            foreach (var item in requests)
            {
                string[] currentChange = item.Split(':');

                UserRequest currentRequest = new UserRequest
                {
                    userName = currentChange[0],
                    role = currentChange[1],
                    change = currentChange[2],
                    currentValue = currentChange[3],
                    newValue = currentChange[4]
                };

                userRequests.Add(currentRequest);
            }
            return userRequests;

        }

        public static void ExecuteChangeRequest(UserRequest request)
        {
            var users = GetAllUsersToList();


            foreach (var user in users.Where(u => u.userName == request.userName))
            {
                if(request.change == "role" || request.change == "Role")
                {
                    user.role = request.newValue;
                }
                else if (request.change == "Salary" || request.change == "salary")
                {
                    user.salary = Convert.ToInt32(request.newValue);
                }
            }

            OverwriteSavedUsers(users);

        }

        public static void OverwriteSavedChangeRequests(List<UserRequest> requests)
        {
            NewAdminlogTxt();
            requests.RemoveAt(0);
            foreach (var item in requests)
            {
                using (StreamWriter sw = new StreamWriter(AdminLog, true))
                {
                    string request = item.userName + ":" + item.role + ":" + item.change + ":" + item.currentValue + ":" + item.newValue;
                    
                    sw.WriteLine(request);
                    sw.Close();
                   
                };

            }

        }

        public static void NewAdminlogTxt()
        {

            using (StreamWriter sw = new StreamWriter(AdminLog, false))
            {
                
                sw.WriteLine("username:role:change:currentValue:newValue");
                sw.Close();
            }
        }
        public static int PayOutAllSalaries()
        {
            var users = GetAllUsersToList();

            var totalPay = 0;

            foreach (var user in users)
            {
                user.balance += user.salary;
                totalPay += user.salary;
            }
            OverwriteSavedUsers(users);
            return totalPay;
        }
    }
}
