namespace Application.Services.Contracts
{
    public interface ISubmissionService
    {
        void Create(string code, int contestId, int userId, bool succeeded);
    }
}
