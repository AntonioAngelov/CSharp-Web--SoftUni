namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models.Users;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly LearningSystemDbContext db;

        public UsersService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public UserProfileCourses Courses(string studentId)
            => this.db.Users
            .Where(u => u.Id == studentId)
            .ProjectTo<UserProfileCourses>(new { studentId = studentId })
            .FirstOrDefault();

    }
}
