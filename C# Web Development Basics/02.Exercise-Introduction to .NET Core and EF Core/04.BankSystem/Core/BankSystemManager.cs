namespace _04.BankSystem.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Data;
    using Models;

    public class BankSystemManager
    {
        private const string NoLoggedInUserErrorMessage = "No logged in user. {0}";
        private const string AccountBalanceMessage = "Account {0} has balance of {1}";

        private User loggedUser;
        private BankSystemDbContext context;

        public BankSystemManager(BankSystemDbContext context)
        {
            this.loggedUser = null;
            this.context = context;
        }

        public bool UserLogged
        {
            get { return this.loggedUser != null; }
        }

        public string RegisterUser(List<string> arguments)
        {
            if (this.UserLogged)
            {
                return "Cannot register user while logged in!";
            }

            var username = arguments[0];
            var password = arguments[1];
            var email = arguments[2];

            string usernamePattern = @"^[a-zA-Z][a-zA-Z0-9]{2,}$";
            string passwordPattern = @"^(?=[^\s]*[a-z])(?=[^\s]*[A-Z])(?=[^\s]*\d)[a-zA-Z0-9]{7,}$";
            string emailPattern = @"^([a-zA-Z0-9]+[.-_]?[a-zA-Z0-9]+)(@)[a-zA-Z0-9]+[-]?[a-zA-Z0-9]+(\.[a-zA-Z0-9]{2,})+$";

            if (!Regex.IsMatch(username, usernamePattern))
            {
                return "Incorrect username";
            }
            else if (!Regex.IsMatch(password, passwordPattern))
            {
                return "Incorrect password";
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                return "Incorrect email";
            }

            this.context.Users.Add(new User()
            {
                Username = username,
                Password = password,
                Email = email
            });

            this.context.SaveChanges();

            return $"{username} was registered in the system";
        }

        public string Login(List<string> arguments)
        {
            if (this.UserLogged)
            {
                return "Already logged in!";
            }

            var username = arguments[0];
            var password = arguments[1];

            this.loggedUser = this.context.Users
                .FirstOrDefault(u => u.Username == username
                                     && u.Password == password);

            if (this.UserLogged)
            {
                return $"Succesfully logged in {username}";
            }
            else
            {
                return "Incorrect username / password";
            }
        }

        public string Logout()
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot log out!");
            }
            else
            {
                var loggedOutUsername = this.loggedUser.Username;
                this.loggedUser = null;

                return $"User {loggedOutUsername} successfully logged out";
            }
        }

        public string AddSavingAccount(List<string> arguments)
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot add account!");
            }

            var initialBalance = decimal.Parse(arguments[0]);
            var interestRate = double.Parse(arguments[1]);
            var accountNumber = this.GenerateAccountNumber();

            this.context.SavingAccounts.Add(new SavingAccount(accountNumber, initialBalance, this.loggedUser.Id, interestRate));

            this.context.SaveChanges();

            return $"Succesfully added account with number {accountNumber}";
        }

        public string AddCheckingAccount(List<string> arguments)
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot add account!");
            }

            var initialBalance = decimal.Parse(arguments[0]);
            var fee = decimal.Parse(arguments[1]);
            var accountNumber = this.GenerateAccountNumber();

            this.context.CheckingAccounts.Add(new CheckingAccount(accountNumber, initialBalance, this.loggedUser.Id, fee));

            this.context.SaveChanges();

            return $"Succesfully added account with number {accountNumber}";
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var str = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return str;
        }

        public string ListAccounts()
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot list accounts!");
            }

            var savingAccounts = this.context.SavingAccounts
                .Where(a => a.OwnerId == this.loggedUser.Id)
                .Select(a => new
                {
                    a.AccountNumber,
                    a.Balance
                })
                .OrderBy(a => a.AccountNumber)
                .ToList();

            var checkingAccounts = this.context.CheckingAccounts
                .Where(a => a.OwnerId == this.loggedUser.Id)
                .Select(a => new
                {
                    a.AccountNumber,
                    a.Balance
                })
                .OrderBy(a => a.AccountNumber)
                .ToList();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Accounts for user {this.loggedUser.Username}");

            if (savingAccounts.Any())
            {
               sb.AppendLine($"Saving Accounts:");

                foreach (var acc in savingAccounts)
                {
                    sb.AppendLine($"--{acc.AccountNumber} {acc.Balance}");    
                }
            }
            else
            {
                sb.AppendLine($"Saving Accounts: None");
            }

            if (checkingAccounts.Any())
            {
                sb.AppendLine($"Checking Accounts:");

                foreach (var acc in checkingAccounts)
                {
                    sb.AppendLine($"--{acc.AccountNumber} {acc.Balance}");
                }
            }
            else
            {
                sb.AppendLine($"Checking Accounts: None");
            }

            return sb
                .ToString()
                .Trim();
        }

        public string Deposit(List<string> arguments)
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot make a Deposit!");
            }

            string accNumber = arguments[0];
            decimal moneyAmount = decimal.Parse(arguments[1]);
            
            var acc = this.context
                .Accounts
                .FirstOrDefault(a => a.AccountNumber == accNumber);

            if (acc == null)
            {
                return $"Account with number {accNumber} does not exist!";
            }

            acc.DepositMoney(moneyAmount);
            this.context.SaveChanges();

            return this.GetAccountBalanceMessage(accNumber, acc.Balance);

        }

        public string Withdraw(List<string> arguments)
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot make a Withdraw!");
            }

            string accNumber = arguments[0];
            decimal moneyAmount = decimal.Parse(arguments[1]);

            var acc = this.context
                .Accounts
                .FirstOrDefault(a => a.AccountNumber == accNumber);

            if (acc == null)
            {
                return $"Account with number {accNumber} does not exist!";
            }

            acc.WithdrawMoney(moneyAmount);
            this.context.SaveChanges();

            return this.GetAccountBalanceMessage(accNumber, acc.Balance);

        }

        public string DeductFee(List<string> arguments)
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot make a Withdraw!");
            }

            string accNumber = arguments[0];

            var acc = this.context
                .CheckingAccounts
                .FirstOrDefault(a => a.AccountNumber == accNumber);

            if (acc == null)
            {
                return $"Account with number {accNumber} does not exist!";
            }

            acc.DeductFee();
            this.context.SaveChanges();

            return $"Deducted fee of {accNumber}. Current balance: {acc.Balance}";

        }

        public string AddInterest(List<string> arguments)
        {
            if (!this.UserLogged)
            {
                return this.GetNoLoggedInUserMessage("Cannot make a Withdraw!");
            }

            string accNumber = arguments[0];

            var acc = this.context
                .SavingAccounts
                .FirstOrDefault(a => a.AccountNumber == accNumber);

            if (acc == null)
            {
                return $"Account with number {accNumber} does not exist!";
            }

            acc.AddInterest();
            this.context.SaveChanges();

            return $"Added interest to {accNumber}. Current balance: {acc.Balance}";

        }

        private string GetNoLoggedInUserMessage(string additionalInfo)
        {
            return string.Format(NoLoggedInUserErrorMessage, additionalInfo);
        }

        private string GetAccountBalanceMessage(string accNumber, decimal balance)
        {
            return string.Format(AccountBalanceMessage, accNumber, balance);
        }
    }
}
