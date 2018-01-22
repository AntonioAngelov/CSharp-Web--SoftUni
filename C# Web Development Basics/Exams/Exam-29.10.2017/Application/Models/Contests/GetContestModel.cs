namespace Application.Models.Contests
{
    using Data.Models;
    using Infrastructure.Mapping;
    using Infrastructure.Validation;
    using Infrastructure.Validation.Contests;

    public class GetContestModel : IMapFrom<Contest>
    {
        [Required]
        [ContestName]
        public string Name { get; set; }

        public int UserId { get; set; }
    }
}
