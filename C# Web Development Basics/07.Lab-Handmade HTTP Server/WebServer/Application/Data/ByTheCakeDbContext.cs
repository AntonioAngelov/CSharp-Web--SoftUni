namespace WebServer.Application.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ByTheCakeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ByTheCakeDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Products-orders
            modelBuilder.Entity<ProductOrder>()
                .HasKey(bc => new { bc.ProductId, bc.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(bc => bc.Order)
                .WithMany(b => b.Products)
                .HasForeignKey(bc => bc.OrderId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(bc => bc.Product)
                .WithMany(c => c.Orders)
                .HasForeignKey(bc => bc.ProductId);

            //User-orders
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Owner)
                .HasForeignKey(o => o.OwnerId);
        }
    }
}
