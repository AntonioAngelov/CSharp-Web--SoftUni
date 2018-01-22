namespace _04.BankSystem.Models
{
    using System;

    public abstract class Account
    {
        public Account()
        {
            
        }

        public Account(string accountNumber, decimal balance, int userId)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            OwnerId = userId;
        }

        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int OwnerId { get; set; }
        
        public User Owner { get; set; }

        public void DepositMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("The deposit amount must be positive number!");
            }

            this.Balance += amount;
        }

        public void WithdrawMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("The withdraw amount must be positive number!");
            }

            this.Balance -= amount;
        }
    }
}
