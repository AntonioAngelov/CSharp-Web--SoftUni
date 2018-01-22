namespace GameStore.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Games;

    public class GameService : IGamesService
    {
        public IEnumerable<GameListingAdminModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Select(g => new GameListingAdminModel
                    {
                        Id = g.Id,
                        Name = g.Title,
                        Price = g.Price,
                        Size = g.Size
                    })
                    .ToList();
            }
        }

        public void Add(string title,
            decimal price,
            double size,
            string videoId,
            string thumbnailUrl,
            string description,
            DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Description = description,
                    Price = price,
                    ReleaseDate = releaseDate,
                    Size = size,
                    ThumbnailUrl = thumbnailUrl,
                    Title = title,
                    VideoId = videoId
                };

                db.Add(game);
                db.SaveChanges();
            }
        }
    }
}
