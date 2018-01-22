namespace SoftUniGameStore.Application.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLength)]
        [MaxLength(ValidationConstants.Game.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.VideoIdExactLength)]
        [MaxLength(ValidationConstants.Game.VideoIdExactLength)]
        public string VideoId { get; set; }

        public string Thumbnail { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public DateTime? RealeaseDate { get; set; }

        public List<UserGame> Users { get; set; } = new List<UserGame>();
    }
}
