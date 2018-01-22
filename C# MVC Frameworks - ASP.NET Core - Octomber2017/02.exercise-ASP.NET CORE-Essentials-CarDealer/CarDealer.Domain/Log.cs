namespace CarDealer.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Log
    {
        public int Id { get; set;}

        public string UserId { get; set; }

        public User User { get; set; }

        public LogOperation Operation { get; set; }

        [Required]
        public string ModifiedTable { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
