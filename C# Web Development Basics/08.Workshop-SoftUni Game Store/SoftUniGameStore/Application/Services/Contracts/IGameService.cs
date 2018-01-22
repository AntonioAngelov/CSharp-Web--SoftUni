namespace SoftUniGameStore.Application.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using ViewModels.Admin;
    using ViewModels.Shopping;

    public interface IGameService
    {
        void Create(
            string title,
            string description,
            string thumbnail,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate);

        bool Exists(int id);

        IList<ListGameViewModel> All();

        FullGameInfoViewModel Find(int id);

        void Edit(int id,
            string title,
            string description,
            string thumbnail,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate);

        void Delete(int id);

        IList<ListGameViewModel> GetOwned(string userEmail);

        IEnumerable<GameInCartViewModel> FindGamesInCart(IEnumerable<int> ids);
    }
}
