namespace LearningSystem.Services
{
    using Models.Courses;
    using System.Collections.Generic;

    public interface ICourseService
    {
        IEnumerable<CourseListingModel> AllListing();

        CourseDetailsModel ById(int id);

        void SignUp(int courseId, string username);

        void SignOut(int courseId, string username);
    }
}
