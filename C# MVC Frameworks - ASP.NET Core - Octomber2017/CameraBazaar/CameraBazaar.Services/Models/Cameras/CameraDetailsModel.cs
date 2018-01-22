namespace CameraBazaar.Services.Models.Cameras
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Services.Infrastructure.Mapping;

    public class CameraDetailsModel : IMapFrom<Camera>
    {
        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
        
        public int MinShutterSpeed { get; set; }
        
        public int MaxShutterSpeed { get; set; }

        public MinISO MinISO { get; set; }
        
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }
        
        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }
        
        public string Description { get; set; }

        public User User { get; set; }
    }
}
