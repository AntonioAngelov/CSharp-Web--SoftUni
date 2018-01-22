namespace _01.StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=StudentSystem;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Students-Courses Relations

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);

            //Course-Resources Relatios
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            //Course-Homeworks Relations
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Homeworks)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);

            //Student-Homeworks Relations
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Homeworks)
                .WithOne(h => h.Student)
                .HasForeignKey(h => h.StudentId);

            //Resourse-Licenses Relations
            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Licenses)
                .WithOne(l => l.Resource)
                .HasForeignKey(l => l.ResourceId);
        }
    }
}
