namespace CameraBazaar.Services.Models.Admins
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Infrastructure.Mapping;

    public class UserListingModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
