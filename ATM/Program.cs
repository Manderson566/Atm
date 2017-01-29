using System;
using System.Linq;

namespace ATM
{
    class Program
    {
        static UserInfo userInstance;
        static UserInfo userInstanceNewAcc;
        static AccountInfo balanceInfo;
        static string Read(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }
        static string Pause()
        {           
            Console.WriteLine("Press Enter");
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            using (var db = new ATMContext())
            {
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
                            AccountCreation(db);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private static void CkBalance(ATMContext db)
        {
            Console.WriteLine($"Your Current Balance: {balanceInfo.Balance}");
            var allWithdrawInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id);
            foreach (var withdraw in allWithdrawInfo)
            {
                Console.WriteLine(withdraw);
            }
            Pause();


        }

        private static void AccountCreation(ATMContext db)
        {
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = 0,
                Withdraw = 0,
                Deposit = 0,
                DateTime = DateTime.Now,
                UserInfo = userInstanceNewAcc,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();
        }

        private static void AccountManagement(ATMContext db)
        {
            bool logCK = true;
            while (logCK)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("1) Make a Withdraw");
                Console.WriteLine("2) Deposit Money");
                Console.WriteLine("3) Check Balance");
                Console.WriteLine("4) Logout");
                int choice = int.Parse(Read("> "));

                switch (choice)
                {
                    case 1:
                        Withdraw(db);
                        Pause();
                        break;
                    case 2:
                        Deposit(db);
                        Pause();
                        break;
                    case 3:
                        UserLogin(db);
                        CkBalance(db);
                        break;
                    case 4:
                        logCK = false;
                        break;
                    default:
                        break;
                }
            }

        }

        private static void Deposit(ATMContext db)
        {   //Deposit(db);
            double balance = balanceInfo.Balance;
            var deposit = Read("How much would you like to Deposit? : ");
            double depositAmount = int.Parse(deposit);
            double newBalance = depositAmount + balance;
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalance,
                Deposit = depositAmount,
                UserInfo = userInstance,
                DateTime = DateTime.Now,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();
            Console.WriteLine($" Your new account balance is {newBalance}");
        }

        private static void Withdraw(ATMContext db)
        {
            
            //Withdraw(db);
            double balance = balanceInfo.Balance;
            var withdraw = Read("How much would you like to withdraw? : ");
            double withdrawAmount = int.Parse(withdraw);
            double newBalance = balance - withdrawAmount;
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalance,
                Withdraw = withdrawAmount,
                UserInfo = userInstance,
                DateTime = DateTime.Now,
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
                    balanceInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id).OrderByDescending(x => x.Id).First();
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
            

            UserInfo userInfo = new UserInfo
            {
                Username = userName,
                Password = password,
                JoinDateTime = joinDate,

            };
            db.UserInfo.Add(userInfo);
            db.SaveChanges();
            userInstanceNewAcc = db.UserInfo.Where(x => x.Username == userName).First();

        }
    }
}

