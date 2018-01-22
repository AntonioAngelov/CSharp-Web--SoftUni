namespace Application.Models.Contests
{
    using Infrastructure.Validation;
    using Infrastructure.Validation.Contests;

    public class ContestModel
    {
        [Required]
        [ContestName]
        public string Name { get; set; }
    }
}
