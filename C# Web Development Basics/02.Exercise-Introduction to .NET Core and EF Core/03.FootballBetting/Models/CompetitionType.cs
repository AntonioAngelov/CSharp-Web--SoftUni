﻿namespace _03.FootballBetting.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CompetitionType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
