namespace CameraBazaar.Services.Models.Admins
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Infrastructure.Mapping;
    using System.Collections.Generic;

    public class AdminUserDetailsModel : UserListingModel, IMapFrom<User>
    {
        public IEnumerable<string> Roles { get; set; }
    }
}
