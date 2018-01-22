namespace _05.ShopHierarchy
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmen { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ShopDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Salesman)
                .WithMany(sm => sm.Customers)
                .HasForeignKey(c => c.SalesmanId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<ItemsOrders>()
                .HasKey(io => new {io.ItemId, io.OrderId});

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Orders)
                .WithOne(o => o.Item)
                .HasForeignKey(o => o.ItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Item)
                .HasForeignKey(r => r.ItemId);
        }
    }
}
