namespace _04.BankSystem.Models
{
    public class CheckingAccount : Account
    {
        public CheckingAccount() : base()
        {
        }

        public CheckingAccount(string accountNumber, decimal balance, int userId, decimal fee) : base(accountNumber, balance, userId)
        {
            this.Fee = fee;
        }

        public decimal Fee { get; set; }

        public void DeductFee()
        {
            this.Balance -= this.Fee;
        }
    }
}
