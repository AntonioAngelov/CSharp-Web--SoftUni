namespace Application.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class JudgeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<Contest> Contests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer($"Server=.;Database=JudgeDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Entity<Contest>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contests)
                .HasForeignKey(c => c.UserId);

            builder
                .Entity<Submission>()
                .HasOne(s => s.User)
                .WithMany(u => u.Submissions)
                .HasForeignKey(s => s.UserId);

            builder
                .Entity<Submission>()
                .HasOne(s => s.Contest)
                .WithMany(c => c.Submissions)
                .HasForeignKey(s => s.ContestId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
