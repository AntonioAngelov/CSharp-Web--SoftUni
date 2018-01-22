namespace LearningSystem.Services.Admin.Models.Admins
{
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;

    public class AdminUserListingModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
