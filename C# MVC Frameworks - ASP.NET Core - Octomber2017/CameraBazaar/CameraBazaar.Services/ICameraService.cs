namespace CameraBazaar.Services
{
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Services.Models.Cameras;
    using System.Collections.Generic;

    public interface ICameraService
    {
        IEnumerable<CameraListingModel> AllListing();

        void Create(
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minISO,
            int maxISO,
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightmeterings,
            string description,
            string imageUrl,
            string userId);

        CameraDetailsModel ById(int id);

        bool Exists(int id, string userId);

        void Delete(int id, string userId);
    }
}