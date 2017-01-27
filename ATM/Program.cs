using System;
using System.Linq;

namespace ATM
{
    class Program
    {
        static UserInfo userInstance;
        static AccountInfo balanceInfo;
        static string Read(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            using (var db = new ATMContext())
            {
                LoginorSignUp(db);
                return;
            }


        }

        private static void AccountManagement(ATMContext db)
        {
            bool logCK = true;
            while (logCK)
            {
                Console.Clear();
                Console.WriteLine("1) Make a Withdraw");
                Console.WriteLine("2) Deposit Money");
                Console.WriteLine("2) Logout");
                int choice = int.Parse(Read("> "));

                switch (choice)
                {
                    case 1:
                        Withdraw(db); ;
                        break;
                    case 2:
                        Deposit(db);
                        break;
                    case 3:
                        logCK = false;
                        break;
                    default:
                        break;
                }
            }

        }

        private static void LoginorSignUp(ATMContext db)
        {
            //LoginorSignUp(db);
            //return;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Login");
                Console.WriteLine("2) Create an Account");
                int choice = int.Parse(Read("> "));

                switch (choice)
                {
                    case 1:
                        UserLogin(db);
                        AccountManagement(db);
                        break;
                    case 2:
                        NewUser(db);
                        break;

                    default:
                        break;
                }
            }
        }

        private static void Deposit(ATMContext db)
        {   //Deposit(db);
            UserLogin(db);
            double balance = balanceInfo.Balance;
            var deposit = Read("How much would you like to Deposit? Enter a number.");
            double depositAmount = int.Parse(deposit);
            double newBalance = depositAmount + balance;
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalance,
                Deposit = depositAmount,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();
            Console.WriteLine($" Your new account balance is {newBalance}");
        }

        private static void Withdraw(ATMContext db)
        {
            
            //Withdraw(db);
            double balance = balanceInfo.Balance;
            var withdraw = Read("How much would you like to withdraw? Enter a number.");
            double withdrawAmount = int.Parse(withdraw);
            double newBalance = withdrawAmount - balance;
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalance,
                Withdraw = withdrawAmount,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();
            Console.WriteLine($" Your new account balance is {newBalance}");
        }

        private static void UserLogin(ATMContext db)
        {  //UserLogin(db);
            for (int i = 0; i < 6; i++)
            {
                if (i > 4)
                {
                    Console.WriteLine("Too many attempts. Please try again Later");
                    Environment.Exit(0);
                }
                var userName = Read("Enter your username");
                var password = Read("Enter your password");
                bool userNameTrue = db.UserInfo.Any(u => u.Username == (userName));
                bool pwTrue = db.UserInfo.Any(u => u.Password == (password));
                if (userNameTrue && pwTrue)
                {
                    Console.WriteLine("Login Successfull");
                    userInstance = db.UserInfo.Where(x => x.Username == userName).First();
                    var userId = userInstance.Id;
                    balanceInfo = db.AccountInfo.Where(x => x.Id == userId).First();
                    //balanceinfo.Balance;
                    break;
                }
                else
                {
                    Console.WriteLine("Password or Username is incorrect please try again");

                }
                
            }
                
        }

        private static void NewUser(ATMContext db)
        { // NewUser(db);

            var userName = Read("Enter a new username");
            var password = Read("Enter a password");
            var joinDate = DateTime.Now;
           
            //var userInstance = db.UserInfo.Where(u => u.Username == userName).First();

            UserInfo userInfo = new UserInfo
            {
                Username = userName,
                Password = password,
                JoinDateTime = joinDate,
            };
            db.UserInfo.Add(userInfo);
            db.SaveChanges();            
        }
    }
}

