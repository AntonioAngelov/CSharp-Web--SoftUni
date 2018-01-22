namespace _04.BankSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BankSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<SavingAccount> SavingAccounts { get; set; }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=BankSystem;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User-Accounts
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.OwnerId);

            //Account hierarchy
            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("Account Type")
                .HasValue<SavingAccount>("SavingAccount")
                .HasValue<CheckingAccount>("CheckingAccount");
        }
    }
}
