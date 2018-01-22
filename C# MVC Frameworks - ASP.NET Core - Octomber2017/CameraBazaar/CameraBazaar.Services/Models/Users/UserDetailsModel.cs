namespace CameraBazaar.Services.Models.Users
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Infrastructure.Mapping;
    using CameraBazaar.Services.Models.Cameras;
    using System.Collections.Generic;
    using AutoMapper;

    public class UserDetailsModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public IEnumerable<CameraListingModel> Cameras { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsModel>()
                .ForMember(udm => udm.Cameras, cfg => cfg.MapFrom(u => u.Cameras));
        }
    }
}
