namespace Application.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public List<Submission> Submissions { get; set; } = new List<Submission>();

        public List<Contest> Contests { get; set; } = new List<Contest>();
    }
}
