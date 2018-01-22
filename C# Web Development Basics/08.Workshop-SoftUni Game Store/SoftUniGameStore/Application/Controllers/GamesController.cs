namespace SoftUniGameStore.Application.Controllers
{
    using System.Text;
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;

    public class GamesController : Controller
    {
        private const string DetailsVew = @"games\game-details";

        private readonly IGameService games;

        public GamesController(IHttpRequest request) 
            : base(request)
        {
            this.games = new GameService();
        }

        public IHttpResponse Details(int gameId)
        {
            var game = this.games
                .Find(gameId);

            if (game == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["description"] = game.Description;
            this.ViewData["price"] = $"{game.Price:f2}";
            this.ViewData["size"] = $"{game.Size:f1}";
            this.ViewData["releaseDate"] = game.ReleaseDate.Value.ToString();

            var buttons = new StringBuilder($@"<a class=""btn btn-outline-primary"" href=""/home/all"">Back</a>");

            if (this.Authentication.IsAdmin)
            {
                buttons.AppendLine($@"<a class=""btn btn-warning"" href=""/admin/games/edit/{game.Id}"">Edit</a>")
                    .AppendLine($@"<a class=""btn btn-danger"" href=""/admin/games/delete/{game.Id}"">Delete</a>");
            }

            buttons.AppendLine($@"<a class=""btn btn-primary"" href=""/shopping/add/{game.Id}"">Buy</a>");

            this.ViewData["buttons"] = buttons.ToString();

            return this.FileViewResponse(DetailsVew);

        }
    }
}
