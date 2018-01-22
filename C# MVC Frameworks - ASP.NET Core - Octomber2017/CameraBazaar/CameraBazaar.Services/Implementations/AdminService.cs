namespace CameraBazaar.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using CameraBazaar.Web.Data;
    using Models.Admins;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminService : IAdminService
    {
        private readonly CameraBazaarDbContext db;

        public AdminService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserListingModel> AllUsers()
            => this.db.Users
                .ProjectTo<UserListingModel>()
                .ToList();

        public AdminUserDetailsModel UserById(string id)
            => this.db.Users
                .Where(u => u.Id == id)
                .ProjectTo<AdminUserDetailsModel>()
                .FirstOrDefault();
    }
}
