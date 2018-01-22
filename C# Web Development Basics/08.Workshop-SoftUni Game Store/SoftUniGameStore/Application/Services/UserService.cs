namespace SoftUniGameStore.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;

    public class UserService : IUserService
    {
        public bool Create(string email, string name, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(u => u.Email == email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any();

                var user = new User
                {
                    Email = email,
                    Name = name,
                    Password = password,
                    IsAdmin = isAdmin
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            return true;
        }

        public bool Find(string email, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Email == email && u.Password == password);
            }
        }

        public bool OwnsGame(string userEmail, int gameId)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db.Users
                    .Select(u => new
                    {
                        u.Email,
                        GamesIds = u.Games.Select(g => g.GameId)

                    })
                    .FirstOrDefault(u => u.Email == userEmail);

                return user.GamesIds.Any(id => id == gameId);
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Email == email && u.IsAdmin);
            }
        }

        public int? GetUserId(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db
                    .Users
                    .FirstOrDefault(u => u.Email == email);

                return user?.Id;
            }
        }

        public void AddGames(int userId, IEnumerable<int> gamesIds)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db
                    .Users
                    .Find(userId);

                foreach (var id in gamesIds)
                {
                    user.Games
                        .Add(new UserGame()
                        {
                            GameId = id
                        });
                }

                db.SaveChanges();
            }
        }
    }
}
