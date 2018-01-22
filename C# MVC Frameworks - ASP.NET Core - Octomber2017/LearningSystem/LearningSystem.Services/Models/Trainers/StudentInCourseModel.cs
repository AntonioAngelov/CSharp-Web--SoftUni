namespace LearningSystem.Services.Models.Trainers
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.Linq;

    public class StudentInCourseModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            int courseId = default(int);

            mapper
                .CreateMap<User, StudentInCourseModel>()
                .ForMember(s => s.Grade, cfg => cfg.MapFrom(u => u
                  .Courses
                  .Where(c => c.CourseId == courseId)
                  .Select(c => c.Grade)
                  .FirstOrDefault()));
        }
    }
}
