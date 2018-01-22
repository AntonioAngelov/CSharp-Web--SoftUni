namespace Application.Services
{
    using Contracts;
    using Data;
    using Data.Models;

    public class SubmissionService : ISubmissionService
    {
        private readonly JudgeDbContext db;

        public SubmissionService(JudgeDbContext db)
        {
            this.db = db;
        }

        public void Create(string code, int contestId, int userId,bool succeeded)
        {
            var submission = new Submission
            {
                Code = code,
                ContestId = contestId,
                Succeeded = succeeded,
                UserId = userId
            };

            this.db.Add(submission);
            this.db.SaveChanges();
        }
    }
}
