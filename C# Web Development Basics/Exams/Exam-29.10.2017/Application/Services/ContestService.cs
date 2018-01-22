namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Contests;

    public class ContestService : IContestService
    {
        private readonly JudgeDbContext db;

        public ContestService(JudgeDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, int userId)
        {
            var contest = new Contest
            {
                Name = name,
                UserId = userId
            };

            this.db.Add(contest);
            this.db.SaveChanges();
        }

        public IEnumerable<ContestListingModel> All()
        {
            return this.db.Contests
                .ProjectTo<ContestListingModel>()
                .ToList();
        }

        public GetContestModel GetById(int id)
        {
            return this.db
                .Contests
                .Where(p => p.Id == id)
                .ProjectTo<GetContestModel>()
                .FirstOrDefault();
        }

        public void Update(int id, string name)
        {
            var contest = db.Contests.Find(id);

            if (contest == null)
            {
                return;
            }

            contest.Name = name;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = db.Contests.Find(id);

            if (post == null)
            {
                return;
            }

            this.db.Contests.Remove(post);
            this.db.SaveChanges();
        }

        public IEnumerable<ContestInSubmissionsModel> AllInSubmussion()
        {
            return this.db
                .Contests
                .ProjectTo<ContestInSubmissionsModel>()
                .ToList();
        }
    }
}
