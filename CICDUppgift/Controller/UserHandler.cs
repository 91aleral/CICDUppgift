namespace CICDUppgift.Controller
{
    using CICDUppgift.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class UserHandler
    {
        /// <summary>
        /// User text fil path.
        /// </summary>
        public static string userPath = "..//..//..//..//CICDUppgift//Users.txt";
        /// <summary>
        /// AdminLog text fil path.
        /// Till för admin att se förändringar som användare vill utföra.
        /// </summary>
        private static string AdminLog = "..//..//..//..//CICDUppgift//AdminLog.txt";

        /// <summary>
        /// Loggar in användare, kollar i listan så användare finns och stämmer
        /// Likadant för lösenord.
        /// Stämmer allt, loggas man in, annars ej.
        /// </summary>
        /// <param name="username">Användarnamn</param>
        /// <param name="password">Lösenord</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sparar en förändring som användare vill utföra.
        /// Ex: Ändra Roll eller sin lön.
        /// </summary>
        /// <param name="userName">Användarnamn</param>
        /// <param name="userRole">Roll</param>
        /// <param name="typeOfChange">Typ av förändring</param>
        /// <param name="currentValue">Nuvarande värde (Roll eller lön)</param>
        /// <param name="newValue">Det förändrade värdet.</param>
        public static void SaveChangeRequest(string userName, string userRole, string typeOfChange, string currentValue, string newValue)
        {
            string request = userName + ":" + userRole + ":" + typeOfChange + ":" + currentValue + ":" + newValue;

            using (StreamWriter sw = new StreamWriter(AdminLog, true))
            {
                sw.WriteLine(request);
            }
        }

        /// <summary>
        /// Hämtar alla användare, som sedan sparar till en lista som user objekt.
        /// </summary>
        /// <returns>En användare</returns>
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

        /// <summary>
        /// Kollar om en användare finns i systemet.
        /// </summary>
        /// <param name="username">Användarnamn</param>
        /// <returns>True om hen finns, annars false</returns>
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

        /// <summary>
        /// Radderar en användare om man anger sitt:
        /// Användarnamn och lösenord.
        /// Sedan skrivs allt över i systemet och uppdateras.
        /// </summary>
        /// <param name="username">Användarnamn</param>
        /// <param name="password">Lösenord</param>
        public static void DeleteUser(string username, string password)
        {
            List<User> users = GetAllUsersToList();

            users.RemoveAll(u => u.userName == username && u.password == password);
            OverwriteSavedUsers(users);
        }

        /// <summary>
        /// Lägger till en ny användare.
        /// </summary>
        /// <param name="userName">Användarnamn</param>
        /// <param name="password">Lösenord</param>
        /// <param name="role">Roll</param>
        /// <param name="salary">Lön</param>
        /// <param name="accountType">Konto typ (User / Admin).</param>
        public static void AddNewUser(string userName, string password, string role, int salary, string accountType)
        {
            string user = GetNewUserID() + ":" + userName + ":" + password + ":" + role + ":" + salary + ":" + 0 + ":" + accountType;
            
            using (StreamWriter sw = new StreamWriter(userPath, true))
            {
                sw.WriteLine(user);
            }
        }

        /// <summary>
        /// Skriver över sparade användare med ny uppdaterad data.
        /// </summary>
        /// <param name="users">Användare</param>
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
                }
            }
        }

        /// <summary>
        /// Skapar en ny tom sparning av användare, med enbart admin.
        /// </summary>
        public static void NewUserTxt()
        {
            using (StreamWriter sw = new StreamWriter(userPath, false))
            {
                string user = 1 + ":" + "admin1" + ":" + "admin1234" + ":" + "Admin" + ":" + 0 + ":" + 0 + ":" + "Admin";
                sw.WriteLine(user);
            }
        }

        /// <summary>
        /// När en ny användare skapas, får hen ett uppdaterat ID i systemet.
        /// </summary>
        /// <returns>ID + 1</returns>
        public static int GetNewUserID()
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
            var lastID = Users.Last().Split(':');
            int ID = Convert.ToInt32(lastID[0]);

            return ID + 1;
        }

        /// <summary>
        /// Kontrollerar att man enbart får skriva siffror
        /// </summary>
        /// <returns>Input</returns>
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

        /// <summary>
        /// Kontrollerar att man enbart får skriva bokstäver
        /// </summary>
        /// <returns>Input</returns>
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

        /// <summary>
        /// Kontrollerar att man enbart får skriva både siffror och bokstäver.
        /// </summary>
        /// <returns>Input</returns>
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

        /// <summary>
        /// Hämtar data från AdminLog filen för att se det användaren vill förändra
        /// på sitt konto.
        /// </summary>
        /// <returns>Användarens förfrågan</returns>
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

        /// <summary>
        /// Skriver över gammal data till ny data som användaren vill förändra.
        /// </summary>
        /// <param name="request">Förfrågan</param>
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

        /// <summary>
        /// Skriver över AdminLog med ny data
        /// </summary>
        /// <param name="requests">Förfrågan</param>
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
                }
            }
        }

        /// <summary>
        /// Skapar en ny tom AdminLog fil
        /// </summary>
        public static void NewAdminlogTxt()
        {
            using (StreamWriter sw = new StreamWriter(AdminLog, false))
            {
                sw.WriteLine("username:role:change:currentValue:newValue");
            }
        }

        /// <summary>
        /// Betalar ut lön till alla i systemet.
        /// </summary>
        /// <returns>Den totala lönen</returns>
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