using System;
using System.Linq;

namespace ATM
{
    class Program
    {
        static UserInfo userInstance;
        static UserInfo userInstanceNewAcc;
        static AccountInfo balanceInfo;
        static string userName;
        static string Read(string input)
        {
            Console.Write(input.ToUpper());
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
        private static void AccountActivity(ATMContext db)
        {
            bool logCK = true;
            while (logCK)
            {
                userInstance = db.UserInfo.Where(x => x.Username == userName).First();
                balanceInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id).OrderByDescending(x => x.Id).First();
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("1) Check Balance");
                Console.WriteLine("2) Withdraw Activity");
                Console.WriteLine("3) Deposit Activity");
                Console.WriteLine("4) Back");
                int choice = int.Parse(Read("> "));

                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"Your Current Balance: ${balanceInfo.Balance}");
                        Pause();
                        break;
                    case 2:
                        var allWithdrawInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id);
                        foreach (var withdraw in allWithdrawInfo)
                        {
                            Console.WriteLine($"You Withdrew: ${withdraw.Withdraw} On   {withdraw.DateTime }");
                        }
                        Pause();
                        break;
                    case 3:
                        var allDepositInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id);
                        foreach (var Deposit in allDepositInfo)
                        {
                            Console.WriteLine($"You Deposited: ${Deposit.Deposit} On   {Deposit.DateTime }");
                        }
                        Pause();
                        break;
                    case 4:
                        logCK = false;
                        break;
                    default:
                        break;
                }
            }            
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
                Console.WriteLine("3) Account Activity");
                Console.WriteLine("4) Transfer Money");
                Console.WriteLine("5) Logout");
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
                        AccountActivity(db);
                        break;
                    case 4:
                        Transfer(db);
                        break;
                    case 5:
                        logCK = false;
                        break;
                    default:
                        break;
                }
            }
        }
        private static void Deposit(ATMContext db)
        {   //Deposit(db);
            userInstance = db.UserInfo.Where(x => x.Username == userName).First();
            balanceInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id).OrderByDescending(x => x.Id).First();
            double balance = balanceInfo.Balance;
            var deposit = Read("How much would you like to Deposit? : ");
            double depositAmount = int.Parse(deposit);
            double newBalance;
            newBalance = depositAmount + balance;
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalance,
                Deposit = depositAmount,
                UserInfo = userInstance,
                DateTime = DateTime.Now,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();
            Console.WriteLine($" Your new account balance is ${newBalance}");
        }
        private static void Withdraw(ATMContext db)
        {
            //Withdraw(db);
            userInstance = db.UserInfo.Where(x => x.Username == userName).First();
            balanceInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id).OrderByDescending(x => x.Id).First();
            double balance = balanceInfo.Balance;
            var withdraw = Read("How much would you like to withdraw? : ");
            double withdrawAmount = int.Parse(withdraw);
            double newBalance;
            if (withdrawAmount > balance)
            {
                Console.WriteLine("You overdrew your account. You will be charged a $15 fee.");
                double overdrawnBalance = withdrawAmount + 15;
                newBalance = balance - overdrawnBalance;
            }
            else
            {
                newBalance = balance - withdrawAmount;
            }
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalance,
                Withdraw = withdrawAmount,
                UserInfo = userInstance,
                DateTime = DateTime.Now,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();
            Console.WriteLine($" Your new account balance is ${newBalance}");
        }
        private static void Transfer(ATMContext db)
        {
            userName = Read("Enter the name of the account you would like to transfer to: ");
            bool userNameTrue = db.UserInfo.Any(u => u.Username == (userName));
            while (userNameTrue == false)
            {
                Console.WriteLine("That user does not exist.");
                userName = Read("Enter the name of the account you would like to transfer to: ");
                userNameTrue = db.UserInfo.Any(u => u.Username == (userName));
            }
            userInstance = db.UserInfo.Where(x => x.Username == userName).First();
            balanceInfo = db.AccountInfo.Where(y => y.UserInfo.Id == userInstance.Id).OrderByDescending(x => x.Id).First();
            double balanceTransfer = balanceInfo.Balance;
            var deposit = Read("How much would you like to Transfer?: ");
            double depositAmount = int.Parse(deposit);           
            double newBalanceTransferTO = depositAmount + balanceTransfer;
            AccountInfo accountInfo = new AccountInfo
            {
                Balance = newBalanceTransferTO,
                Deposit = depositAmount,
                UserInfo = userInstance,
                DateTime = DateTime.Now,
            };
            db.AccountInfo.Add(accountInfo);
            db.SaveChanges();

            Console.WriteLine("Please verify your account information to make the transfer.");
            UserLogin(db);
            double balance = balanceInfo.Balance;
            var withdraw = deposit;
            double withdrawAmount = int.Parse(withdraw);
            double newBalance;
            if (withdrawAmount > balance)
            {
                Console.WriteLine("You overdrew your account. You will be charged a $15 fee.");
                double overdrawnBalance = withdrawAmount + 15;
                newBalance = balance - overdrawnBalance;
            }
            else
            {
                newBalance = balance - withdrawAmount;
            }
            AccountInfo accountInfo2 = new AccountInfo
            {
                Balance = newBalance,
                Withdraw = withdrawAmount,
                UserInfo = userInstance,
                DateTime = DateTime.Now,
            };
            db.AccountInfo.Add(accountInfo2);
            db.SaveChanges();
            Console.WriteLine($" Your new account balance is ${newBalance}");
            Pause();
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
                    userName = Read("Enter your username: ");
                var password = Read("Enter your password: ");
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
            
            var userName = Read("Enter a new username: ");
            bool userNameTrue = db.UserInfo.Any(u => u.Username == (userName));
                while (userNameTrue == true)
                {
                    Console.WriteLine("That Username is already taken.");
                    userName = Read("Enter a new username: ");
                    userNameTrue = db.UserInfo.Any(u => u.Username == (userName));
            }                    
            var password = Read("Enter a password: ");
            UserInfo userInfo = new UserInfo
            {
                Username = userName,
                Password = password,
                JoinDateTime = DateTime.Now,
            };
            db.UserInfo.Add(userInfo);
            db.SaveChanges();
            userInstanceNewAcc = db.UserInfo.Where(x => x.Username == userName).First();
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
    }
}

