namespace CameraBazaar.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using CameraBazaar.Services.Models.Users;
    using CameraBazaar.Web.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserDetailsModel ById(string id)
            => this.db.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDetailsModel>()
                .FirstOrDefault();
    }
}
