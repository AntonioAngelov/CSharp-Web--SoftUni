namespace Application.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contest
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
