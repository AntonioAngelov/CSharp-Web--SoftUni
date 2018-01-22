namespace SoftUniGameStore.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using ViewModels.Admin;
    using ViewModels.Shopping;

    public class GameService : IGameService
    {
        public void Create(string title,
            string description,
            string thumbnail,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Description = description,
                    Price = price,
                    RealeaseDate = releaseDate,
                    Size = size,
                    Thumbnail = thumbnail,
                    Title = title,
                    VideoId = videoId
                };

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public IList<ListGameViewModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                var games = db.Games
                    .Select(g => new ListGameViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Price = g.Price,
                        Size = g.Size,
                        Thumbnail = g.Thumbnail,
                        Description = g.Description
                    })
                    .ToList();

                return games;
            }
        }

        public FullGameInfoViewModel Find(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Where(g => g.Id == id)
                    .Select(g => new FullGameInfoViewModel()
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Description = g.Description,
                        Price = g.Price,
                        ReleaseDate = g.RealeaseDate.Value,
                        Size = g.Size,
                        Thumbnail = g.Thumbnail,
                        VideoId = g.VideoId
                    })
                    .FirstOrDefault();
            }
        }

        public void Edit(int id,
            string title,
            string description,
            string thumbnail,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var gameToEdit = db.Games.Find(id);

                gameToEdit.Title = title;
                gameToEdit.Description = description;
                gameToEdit.Thumbnail = thumbnail;
                gameToEdit.Price = price;
                gameToEdit.Size = size;
                gameToEdit.VideoId = videoId;
                gameToEdit.RealeaseDate = releaseDate;

                db.SaveChanges();

            }
        }

        public void Delete(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                db.Games
                    .Remove(db.Games
                        .Find(id));

                db.SaveChanges();
            }
        }

        public IList<ListGameViewModel> GetOwned(string userEmail)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Where(g => g.Users
                        .Any(ug => ug.User.Email == userEmail))
                    .Select(g => new ListGameViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Price = g.Price,
                        Size = g.Size,
                        Thumbnail = g.Thumbnail,
                        Description = g.Description
                    })
                    .ToList();
            }
        }

        public bool Exists(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Any(g => g.Id == id);
            }
        }

        public IEnumerable<GameInCartViewModel> FindGamesInCart(IEnumerable<int> ids)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Where(g => ids.Contains(g.Id))
                    .Select(g => new GameInCartViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Description = g.Description,
                        Price = g.Price,
                        Thumbnail = g.Thumbnail
                    })
                    .ToList();
            }
        }
    }
}
