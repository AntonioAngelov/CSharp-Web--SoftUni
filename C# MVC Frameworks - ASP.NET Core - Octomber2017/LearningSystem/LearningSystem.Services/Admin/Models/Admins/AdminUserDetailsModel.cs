namespace LearningSystem.Services.Admin.Models.Admins
{
    using System;
    using System.Collections.Generic;

    public class AdminUserDetailsModel : AdminUserListingModel
    {
        public IEnumerable<string> Roles { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
