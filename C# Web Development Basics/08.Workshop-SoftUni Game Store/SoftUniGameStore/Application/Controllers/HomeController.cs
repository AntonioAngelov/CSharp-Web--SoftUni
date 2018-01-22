namespace SoftUniGameStore.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Services;
    using Services.Contracts;
    using ViewModels.Admin;

    public class HomeController : Controller
    {
        private const string HomeIndexView = @"home\index";

        private readonly IGameService games;

        public HomeController(IHttpRequest request)
            : base(request)
        {
            this.games = new GameService();
        }

        public IHttpResponse Index()
        {
            string BeginingOflineHtml = $@"<div class=""card-group"">";
            string EndOfLineHtml = "</div>";


            IList<ListGameViewModel> allGames = new List<ListGameViewModel>();

            if (this.Request.UrlParameters.ContainsKey("filter")
                && this.Request.UrlParameters["filter"] == "Owned"
                && this.Authentication.IsAuthenticated)
            {
                allGames = games
                    .GetOwned(this.Request.Session.Get<string>(SessionStore.CurrentUserKey));
            }
            else
            {
                allGames = games
                    .All();

            }

            var gamesHtml = new StringBuilder();
            gamesHtml.AppendLine();

            for (int i = 0; i < allGames.Count(); i++)
            {
                if (i != 0 && i % 3 == 0)
                {
                    gamesHtml.AppendLine(EndOfLineHtml);
                }


                if (i % 3 == 0)
                {
                    gamesHtml.AppendLine(BeginingOflineHtml);
                }

                var adminButtons = string.Empty;

                if (this.Authentication.IsAdmin)
                {
                    adminButtons = $@"<a class=""card-button btn btn-warning"" name=""edit"" href=""/admin/games/edit/{allGames[i].Id}"">Edit</a>
                                      <a class=""card-button btn btn-danger"" name=""delete"" href=""/admin/games/delete/{allGames[i].Id}"">Delete</a>";
                }

                gamesHtml.Append($@"<div class=""card col-4 thumbnail"">
                        <img class=""card-image-top img-fluid img-thumbnail"" onerror=""this.src='{allGames[i].Thumbnail}';""
                        src=""{allGames[i].Thumbnail}"">

                        <div class=""card-body"">
                            <h4 class=""card-title"">{allGames[i].Title}</h4>
                            <p class=""card-text""><strong>Price</strong> - {allGames[i].Price:f2}&euro;</p>
                            <p class=""card-text""><strong>Size</strong> - {allGames[i].Size:f1} GB</p>
                            <p class=""card-text"">{allGames[i].Description}</p>
                        </div>

                        <div class=""card-footer"">
                            {adminButtons}

                            <a class=""card-button btn btn-outline-primary"" name=""info"" href=""/games/details/{allGames[i].Id}"">Info</a>
                            <a class=""card-button btn btn-primary"" name=""buy"" href=""/shopping/add/{allGames[i].Id}"">Buy</a>
                        </div>
                    </div>");
            }

            var gamesString = gamesHtml.ToString();

            if (gamesString.EndsWith(BeginingOflineHtml))
            {
                gamesString = gamesString.Substring(0, gamesString.Length - BeginingOflineHtml.Length);

            }
            else if (!gamesString.EndsWith(EndOfLineHtml))
            {
                gamesString += EndOfLineHtml;
            }

            this.ViewData["games"] = gamesString;

            return this.FileViewResponse(HomeIndexView);
        }
    }
}