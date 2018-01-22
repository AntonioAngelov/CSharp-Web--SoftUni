namespace _04.BankSystem.Core
{
    using System;
    using System.Linq;

    public class Engine
    {
        private BankSystemManager bankManager;

        public Engine(BankSystemManager bankManager)
        {
            this.bankManager = bankManager;
        }

        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine()
                    .Split(' ')
                    .ToList();

                var command = input[0];
                var arguments = input
                    .Skip(1)
                    .ToList();

                var message = string.Empty;

                switch (command)
                {
                    case "Register":
                        message = this.bankManager.RegisterUser(arguments);
                        break;
                    case "Login":
                        message = this.bankManager.Login(arguments);
                        break;
                    case "Logout":
                        message = this.bankManager.Logout();
                        break;
                    case "Add":
                        var accType = arguments[0];
                        arguments = arguments
                            .Skip(1)
                            .ToList();

                        if (accType == "SavingsAccount")
                        {
                            message = this.bankManager.AddSavingAccount(arguments);
                        }
                        else if (accType == "CheckingAccount")
                        {
                            message = this.bankManager.AddCheckingAccount(arguments);
                        }

                        break;
                    case "ListAccounts":
                        message = this.bankManager.ListAccounts();
                        break;
                    case "Deposit":
                        message = this.bankManager.Deposit(arguments);
                        break;
                    case "Withdraw":
                        message = this.bankManager.Withdraw(arguments);
                        break;
                    case "DeductFee":
                        message = this.bankManager.DeductFee(arguments);
                        break;
                    case "AddInterest":
                        message = this.bankManager.AddInterest(arguments);
                        break;
                    default:
                        message = "Incorrect command passed!";
                        break;
                }

                Console.WriteLine(message);

            }
        }
    }
}
