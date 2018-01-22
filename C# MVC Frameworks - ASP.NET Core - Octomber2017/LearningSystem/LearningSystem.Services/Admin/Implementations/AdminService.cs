namespace LearningSystem.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using Models.Admins;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminService : IAdminService
    {
        private readonly LearningSystemDbContext db;

        public AdminService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingModel> UserListing()
           => this.db.Users
                .ProjectTo<AdminUserListingModel>()
                .ToList();

        public AdminUserDetailsModel GetUserById(string id)
            => this.db.Users.Where(u => u.Id == id)
                .ProjectTo<AdminUserDetailsModel>()
                .FirstOrDefault();
    }
}
