namespace SoftUniGameStore.Application.ViewModels.Admin
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Utilities;

    public class FullGameInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLength,
            ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Game.TitleMaxLength,
            ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        [GameTitle]
        public string Title { get; set; }

        [Display(Name = "YouTube Video URL")]
        [Required]
        [MinLength(ValidationConstants.Game.VideoIdExactLength,
            ErrorMessage = ValidationConstants.ExactLengthErrorMessage)]
        [MaxLength(ValidationConstants.Game.VideoIdExactLength,
            ErrorMessage = ValidationConstants.ExactLengthErrorMessage)]
        public string VideoId { get; set; }

        public string Thumbnail { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.DescriptionMinLength,
            ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Realease Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
    }
}
