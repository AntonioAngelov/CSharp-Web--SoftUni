namespace LearningSystem.Services.Admin
{
    using System;

    public interface ICourseService
    {
        void Create(
            string name,
            string description,
            DateTime startDate,
            DateTime endDate,
            string trainerId);

    }
}
