namespace GameStore.App.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using Models.Games;

    public interface IGamesService
    {
        IEnumerable<GameListingAdminModel> All();

        void Add(
            string title,
            decimal price,
            double size,
            string videoId,
            string thumbnailUrl,
            string description,
            DateTime releaseDate);
    }
}
