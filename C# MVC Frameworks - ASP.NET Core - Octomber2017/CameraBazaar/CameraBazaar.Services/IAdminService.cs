namespace CameraBazaar.Services
{
    using CameraBazaar.Services.Models.Admins;
    using System.Collections.Generic;

    public interface IAdminService
    {
        IEnumerable<UserListingModel> AllUsers();

        AdminUserDetailsModel UserById(string id);
    }
}
