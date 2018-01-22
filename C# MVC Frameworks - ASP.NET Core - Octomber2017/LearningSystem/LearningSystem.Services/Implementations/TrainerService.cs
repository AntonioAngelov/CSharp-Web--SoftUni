namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models.Trainers;
    using System.Collections.Generic;
    using System.Linq;
    using LearningSystem.Data.Models;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public TrainerCoursesModel ById(int id)
            => this.db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<TrainerCoursesModel>()
                .FirstOrDefault();

        public IEnumerable<TrainerCoursesModel> Courses(string trainerId)
            => this.db.Courses
                .Where(c => c.TrainerId == trainerId)
                .ProjectTo<TrainerCoursesModel>()
                .ToList();

        public bool IsTrainer(int courseId, string trainerId)
            => this.db.Courses
                .Any(c => c.Id == courseId && c.TrainerId == trainerId);

        public IEnumerable<StudentInCourseModel> StudentsInCourse(int courseId)
            => this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(s => s.User))
                .ProjectTo<StudentInCourseModel>(new { courseId })
                .ToList();

        public bool AddGrade(int courseId, string studentId, Grade grade)
        {
            var studentIncourse = this.db.Find<UserCourse>(studentId, courseId);

            if (studentIncourse == null)
            {
                return false;
            }

            studentIncourse.Grade = grade;

            this.db.SaveChanges();

            return true;
        }
    }
}
