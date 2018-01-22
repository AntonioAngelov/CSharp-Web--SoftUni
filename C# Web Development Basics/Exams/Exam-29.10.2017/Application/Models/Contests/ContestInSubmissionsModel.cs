namespace Application.Models.Contests
{
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ContestInSubmissionsModel : IMapFrom<Contest>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
