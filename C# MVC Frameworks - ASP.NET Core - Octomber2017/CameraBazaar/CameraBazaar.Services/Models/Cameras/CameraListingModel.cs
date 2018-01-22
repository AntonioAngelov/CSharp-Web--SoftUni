namespace CameraBazaar.Services.Models.Cameras
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Services.Infrastructure.Mapping;

    public class CameraListingModel : IMapFrom<Camera>
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }
        
        public string Model { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
