namespace CameraBazaar.Web.Models.Cameras
{
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Services.Infrastructure.Mapping;
    using CameraBazaar.Services.Models.Cameras;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CameraFormModel : IMapFrom<CameraDetailsModel>
    {
        [Display(Name = "Make:")]
        public CameraMake Make { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Z\d-]+",ErrorMessage = "Model must contains only uppercase letters, digits and \"-\".")]
        public string Model { get; set; }
        
        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(1, 30)]
        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }
        
        [Display(Name = "Min ISO")]
        public MinISO MinISO { get; set; }

        [Range(200, 409600)]
        [Display(Name = "Max ISO")]
        public int MaxISO { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Video Resolution")]
        public string VideoResolution { get; set; }
        
        [Display(Name = "Light Metering")]
        public IEnumerable<LightMetering> LightMeterings { get; set; }

        [Required]
        [MaxLength(6000)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
