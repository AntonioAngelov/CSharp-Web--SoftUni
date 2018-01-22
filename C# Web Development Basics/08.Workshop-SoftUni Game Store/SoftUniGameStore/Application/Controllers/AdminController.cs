namespace SoftUniGameStore.Application.Controllers
{
    using System;
    using System.Linq;
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using ViewModels.Admin;

    public class AdminController : Controller
    {
        private const string AddGameView = @"admin\add-game";
        private const string ListGamesView = @"admin\list-games";
        private const string EditGameView = @"admin\edit-game";
        private const string DeleteGameView = @"admin\delete-game";

        private const string ListGamesRedirectView = "/admin/games/list";

        private readonly IGameService games;

        public AdminController(IHttpRequest request)
            : base(request)
        {
            this.games = new GameService();
        }

        public IHttpResponse Add()
        {
            if (!this.Authentication.IsAdmin)
            {
                return RedirectResponse(HomePath);
            }
            else
            {
                return FileViewResponse(AddGameView);
            }
        }

        public IHttpResponse Add(AddGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return RedirectResponse(HomePath);
            }

            if (!this.ValidateModel(model))
            {
                return this.Add();
            }

            this.games.Create(
                model.Title,
                model.Description,
                model.Thumbnail,
                model.Price,
                model.Size,
                model.VideoId,
                model.ReleaseDate.Value);

            return this.RedirectResponse(ListGamesRedirectView);
        }

        public IHttpResponse List()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var allGames = this.games
                .All()
                .Select(g => $@"<tr class=""table-warning"">
                                     <th scope=""row"">{g.Id}</th>
                                     <td>{g.Title}</td>
                                     <td>{g.Size:F1} GB</td>
                                     <td>{g.Price:F2} &euro;</td>
                                     <td>
                                        <a class=""btn btn-warning"" href=""/admin/games/edit/{g.Id}"">Edit</a>
                                        <a class=""btn btn-danger"" href=""/admin/games/delete/{g.Id}"">Delete</a>
                                    </td>
                                </tr>");

            var allGamesAsString = string.Join(Environment.NewLine, allGames);

            this.ViewData["games"] = allGamesAsString;

            return this.FileViewResponse(ListGamesView);
        }

        public IHttpResponse Edit(int id)
        {
            return this.ShowUpdateView(id, EditGameView);
        }

        public IHttpResponse Edit(FullGameInfoViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return RedirectResponse(HomePath);
            }

            var gameId = int.Parse(this.Request.UrlParameters["id"]);

            if (!this.ValidateModel(model))
            {
                return this.Edit(gameId);
            }

            this.games
                .Edit(gameId, model.Title,
                        model.Description, model.Thumbnail,
                        model.Price, model.Size, model.VideoId, 
                        model.ReleaseDate.Value);

            return this.RedirectResponse(ListGamesRedirectView);
        }

        public IHttpResponse Delete(int id)
        {
            return this.ShowUpdateView(id, DeleteGameView);
        }

        private IHttpResponse ShowUpdateView(int id, string view)
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var game = this.games
                .Find(id);

            if (game == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["thumbnail"] = game.Thumbnail;
            this.ViewData["price"] = string.Format("{0:F2}", game.Price);
            this.ViewData["size"] = string.Format("{0:F1}", game.Size);
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["release-date"] = game.ReleaseDate.Value.ToString("yyyy-MM-dd");

            return this.FileViewResponse(view);
        }

        public IHttpResponse Delete()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var gameId = int.Parse(Request.UrlParameters["id"]);

            this.games
                .Delete(gameId);

            return RedirectResponse(ListGamesRedirectView);
        }

    }
}
