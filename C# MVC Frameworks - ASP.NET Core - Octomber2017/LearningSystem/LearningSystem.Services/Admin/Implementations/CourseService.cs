namespace LearningSystem.Services.Admin.Implementations
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using System;
    using System.Linq;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public void Create(
            string name,
            string description, 
            DateTime startDate,
            DateTime endDate, 
            string trainerId)
        {
            var trainer = this.db.Users
                .FirstOrDefault(u => u.Id == trainerId);

            if (trainer == null)
            {
                return;
            }

            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Trainer = trainer
            };

            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }
    }
}
