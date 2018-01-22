namespace GameStore.App.Controllers
{
    using System.Linq;
    using Models.Games;
    using Services;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Contracts;

    public class AdminController : BaseController
    {
        public const string GameError = "<p>Check your form for errors.</p><p>Title has to begin with uppercase letter and has length between 3 and 100 symbols (inclusive).</p><p>Price must be a positive number with precision up to 2 digits after floating point.</p><p>Size must be a positive number with precision up to 1 digit after floating point.</p><p>Videos should only be from YouTube.</p><p>Thumbnail URL should be a plain text starting with http://, https://.</p><p>Description must be at least 20 symbols.</p>";

        private IGamesService games;

        public AdminController()
        {
            this.games = new GameService();
        }

        public IActionResult All()
        {
            if (!this.IsAdmin)
            {
                return this.Redirect(HomePath);
            }

            var games = this.games
                .All()
                .Select(g => $@"
                    <tr class=""table-warning"">
                        <th scope=""row"">{g.Id}</th>
                        <td>{g.Name}</td>
                        <td>{g.Size:F1} GB</td>
                        <td>{g.Price:F2} &euro;</td>
                        <td>
                            <a href=""/admin/edit?id={g.Id}"" class=""btn btn-warning btn-sm"">Edit</a>
                            <a href=""/admin/delete?id={g.Id}"" class=""btn btn-danger btn-sm"">Delete</a>
                        </td>
                    </tr>");

            this.ViewModel["games"] = string.Join(string.Empty, games);

            return this.View();
        }

        public IActionResult Add()
        {
            if (!this.IsAdmin)
            {
                return this.Redirect(HomePath);
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(GameAdminModel model)
        {
            if (!this.IsAdmin)
            {
                return this.Redirect(HomePath);
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError(GameError);

                return this.View();
            }

            this.games.Add(model.Title,
                model.Price,
                model.Size,
                model.VideoId,
                model.ThumbnailUrl,
                model.Description,
                model.ReleaseDate);

            return this.Redirect("/admin/all");
        }
    }
}
