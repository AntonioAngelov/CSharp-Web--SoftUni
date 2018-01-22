namespace _04.BankSystem.Models
{
    public class SavingAccount : Account
    {

        public SavingAccount() : base()
        {
        }

        public SavingAccount(string accountNumber, decimal balance, int userId, double interestRate) : base(accountNumber, balance, userId)
        {
            this.InterestRate = interestRate;
        }

        public double InterestRate { get; set; }

        public void AddInterest()
        {
            this.Balance += this.Balance * (decimal)this.InterestRate;
        }

    }
}
