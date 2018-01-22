using LearningSystem.Services.Models.Trainers;
using System.Collections.Generic;

namespace LearningSystem.Web.Models.Trainers
{
    public class StudentInCourseViewModel
    {
        public IEnumerable<StudentInCourseModel> Students { get; set; }

        public TrainerCoursesModel Course { get; set; }
    }
}
