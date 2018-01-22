namespace Application.Controllers
{
    using System.Linq;
    using Models.Contests;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Contracts;

    public class ContestsController : BaseController
    {
        private readonly IContestService contests;

        public ContestsController(IContestService contests)
        {
            this.contests = contests;
        }

        private const string CreateError =
            "<p>Contest name must begin with uppercase letter and has length between 3 and 100 symbols (inclusive)</p>";

        public IActionResult Create()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ContestModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError(CreateError);
                return this.View();
            }

            this.contests.Create(model.Name, this.Profile.Id);

            return this.Redirect("/contests/all");
        }

        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            var rows = this.contests
                .All()
                .Select(c => $@"
                    <tr>
                        <td>{c.Name}</td>
                        <td>{c.SubmissionsCount}</td>
                        <td>
                            {this.GetEditAndDeleteButtons(c.UserId, c.Id)}
                        </td>
                    </tr>");

            this.ViewModel["contests"] = rows.Any() ? string.Join(string.Empty, rows) : string.Empty;

            return this.View();
        }

        private string GetEditAndDeleteButtons(int userId, int contestId)
        {
            if (this.IsAdmin || this.Profile.Id == userId)
            {
                return $@"<a href=""/contests/edit?id={contestId}"" class=""btn btn-sm btn-warning"" >Edit</a>  
                          <a href = ""/contests/delete?id={contestId}"" class=""btn btn-sm btn-danger"">Delete</a>";
            }

            return string.Empty;
        }

        public IActionResult Edit(int id)
            => this.PrepareEditAndDeleteView(id)
               ?? this.View();

        private IActionResult PrepareEditAndDeleteView(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            var contest = this.contests.GetById(id);

            if (contest == null)
            {
                return this.NotFound();
            }

            if (!this.IsAdmin && this.Profile.Id != contest.UserId)
            {
                return this.RedirectToHome();
            }

            this.ViewModel["name"] = contest.Name;

            return null;
        }

        [HttpPost]
        public IActionResult Edit(int id, ContestModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }


            if (!this.IsValidModel(model))
            {
                this.ShowError(CreateError);
                return this.View();
            }

            this.contests.Update(id, model.Name);

            return this.Redirect("/contests/all");
        }

        public IActionResult Delete(int id)
        {
            this.ViewModel["id"] = id.ToString();

            return this.PrepareEditAndDeleteView(id) ?? this.View();
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            this.contests.Delete(id);

            return this.Redirect("/contests/all");
        }

    }
}
