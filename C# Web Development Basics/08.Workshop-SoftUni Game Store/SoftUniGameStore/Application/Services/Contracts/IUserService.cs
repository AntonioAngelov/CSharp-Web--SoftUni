namespace SoftUniGameStore.Application.Services.Contracts
{
    using System.Collections.Generic;

    public interface IUserService
    {
        bool Create(string email, string name, string password);

        bool Find(string email, string password);

        bool IsAdmin(string email);

        bool OwnsGame(string userEmail, int gameId);

        int? GetUserId(string email);

        void AddGames(int userId, IEnumerable<int> gamesIds);
    }
}
