namespace LearningSystem.Services.Models.Courses
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseDetailsModel : CourseListingModel, IHaveCustomMapping
    {
        public string TrainerId { get; set; }

        public string TrainerName { get; set; }

        public int NumberOfStudents { get; set; }

        public IEnumerable<string> StudentsInCourse { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Course, CourseDetailsModel>()
                .ForMember(cdm => cdm.TrainerName, cfg => cfg.MapFrom(c => c.Trainer.Name))
                .ForMember(cdm => cdm.NumberOfStudents, cfg => cfg.MapFrom(c => c.Students.Count))
                .ForMember(cdm => cdm.StudentsInCourse, cfg => cfg.MapFrom(c => c.Students.Select(sc => sc.User.UserName)));
        }
    }
}
