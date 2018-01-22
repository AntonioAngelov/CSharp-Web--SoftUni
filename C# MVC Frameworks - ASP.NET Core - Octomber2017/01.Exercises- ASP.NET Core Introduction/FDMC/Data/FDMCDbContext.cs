namespace FDMC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FDMCDbContext : DbContext
    {
        public FDMCDbContext(DbContextOptions<FDMCDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
