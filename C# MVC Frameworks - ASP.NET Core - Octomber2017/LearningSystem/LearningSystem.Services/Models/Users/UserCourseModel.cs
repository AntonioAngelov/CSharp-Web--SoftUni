namespace LearningSystem.Services.Models.Users
{
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System;
    using AutoMapper;
    using System.Linq;

    public class UserCourseModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            string studentId = null;
            mapper
                .CreateMap<Course, UserCourseModel>()
                .ForMember(u => u.Grade, cfg => cfg
                    .MapFrom(c => c.Students
                        .Where(s => s.UserId == studentId)
                        .Select(s => s.Grade)
                        .FirstOrDefault()));
        }
    }
}
