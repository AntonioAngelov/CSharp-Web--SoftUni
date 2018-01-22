namespace Application.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Contests;

    public interface IContestService
    {
        void Create(string name, int userId);

        IEnumerable<ContestListingModel> All();

        GetContestModel GetById(int id);

        void Update(int id, string name);

        void Delete(int id);

        IEnumerable<ContestInSubmissionsModel> AllInSubmussion();
    }
}
