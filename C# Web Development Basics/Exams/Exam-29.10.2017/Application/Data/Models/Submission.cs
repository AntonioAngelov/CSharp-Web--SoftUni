namespace Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Submission
    {
        public int Id { get; set; }

        [MinLength(5)]
        public string Code { get; set; }

        public int ContestId { get; set; }

        public Contest Contest { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool Succeeded { get; set; }
    }
}
