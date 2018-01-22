namespace LearningSystem.Services.Models.Users
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileCourses : IMapFrom<User>, IHaveCustomMapping
    {
        public IEnumerable<UserCourseModel> Courses { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<User, UserProfileCourses>()
                .ForMember(u => u.Courses, cfg => cfg.MapFrom(s => s.Courses.Select(c => c.Course)));
        }
    }
}
