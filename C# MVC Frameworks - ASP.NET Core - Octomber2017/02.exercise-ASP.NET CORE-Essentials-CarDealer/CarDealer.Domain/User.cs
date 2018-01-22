namespace CarDealer.Domain
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public List<Log> Logs { get; set; }
    }
}
