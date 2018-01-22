namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Courses;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseListingModel> AllListing()
            => this.db.Courses
                .ProjectTo<CourseListingModel>()
                .ToList();

        public CourseDetailsModel ById(int id)
            => this.db.Courses
                .Where(c => c.Id == id)
                .ProjectTo<CourseDetailsModel>()
                .FirstOrDefault();

        public void SignUp(int courseId, string username)
        {
            var user = this.db.Users
                .FirstOrDefault(u => u.UserName == username);
            var course = this.db.Courses.Find(courseId);

            if (user == null || course == null)
            {
                return;
            }

            course.Students.Add(new UserCourse
            {
                User = user,
                Course = course
            });
            this.db.SaveChanges();
        }

        public void SignOut(int courseId, string username)
        {
            var user = this.db
                .Users
                .FirstOrDefault(u => u.UserName == username);
            var course = this.db.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == courseId);

            if (user == null || course == null)
            {
                return;
            }

            var userToRemove = course
                .Students
                .Where(c => c.UserId == user.Id)
                .First();
            course.Students.Remove(userToRemove);
            this.db.SaveChanges();
        }
    }
}
