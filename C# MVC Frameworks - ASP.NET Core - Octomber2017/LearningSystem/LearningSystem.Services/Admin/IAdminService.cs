namespace LearningSystem.Services.Admin
{
    using LearningSystem.Services.Admin.Models.Admins;
    using System.Collections.Generic;

    public interface IAdminService
    {
        IEnumerable<AdminUserListingModel> UserListing();

        AdminUserDetailsModel GetUserById(string id);
    }
}
