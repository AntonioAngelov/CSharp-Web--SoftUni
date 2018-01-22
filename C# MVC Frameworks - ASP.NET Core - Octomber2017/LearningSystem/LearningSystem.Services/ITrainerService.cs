namespace LearningSystem.Services
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Trainers;
    using System.Collections.Generic;

    public interface ITrainerService
    {
        IEnumerable<TrainerCoursesModel> Courses(string trainerId);

        bool IsTrainer(int courseId, string trainerId);

        IEnumerable<StudentInCourseModel> StudentsInCourse(int courseId);

        TrainerCoursesModel ById(int id);

        bool AddGrade(int courseId, string studentId, Grade grade);
    }
}
