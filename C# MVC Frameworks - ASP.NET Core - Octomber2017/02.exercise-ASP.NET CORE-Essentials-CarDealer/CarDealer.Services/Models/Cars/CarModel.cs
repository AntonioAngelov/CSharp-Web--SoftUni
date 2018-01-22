namespace CarDealer.Services.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    public class CarModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Display(Name = "Travelled Distance")]
        [Range(0, long.MaxValue, ErrorMessage = "Travelled Distance must be more or equal 0.")]
        public long TravelledDistance { get; set; }
    }
}
