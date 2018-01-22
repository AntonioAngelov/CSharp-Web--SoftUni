namespace CameraBazaar.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Services.Models.Cameras;
    using CameraBazaar.Web.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraListingModel> AllListing()
            => this.db.Cameras
                .ProjectTo<CameraListingModel>()
                .ToList();

        public void Create(
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
            IEnumerable<LightMetering> lightMeterings,
            string description,
            string imageUrl,
            string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }

        public CameraDetailsModel ById(int id)
            => this.db.Cameras
                .Where(c => c.Id == id)
                .ProjectTo<CameraDetailsModel>()
                .FirstOrDefault();

        public bool Exists(int id, string userId)
            => this.db.Cameras.Any(c => c.Id == id && c.UserId == userId);

        public void Delete(int id, string userId)
        {
            var camera = this.db.Cameras
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefault();

            if (camera == null)
            {
                return;
            }

            this.db.Cameras.Remove(camera);
            this.db.SaveChanges();
        }
    }
}
