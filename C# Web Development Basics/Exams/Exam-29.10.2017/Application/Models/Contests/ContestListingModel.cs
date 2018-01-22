namespace Application.Models.Contests
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ContestListingModel : IMapFrom<Contest>, IHaveCustomMapping
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string SubmissionsCount { get; set; }

        public int UserId { get; set; }

        public void Configure(IMapperConfigurationExpression config)
        {
            config
                .CreateMap<Contest, ContestListingModel>()
                .ForMember(hl => hl.SubmissionsCount, cfg => cfg.MapFrom(c => c.Submissions.Count));
        }
    }
}
