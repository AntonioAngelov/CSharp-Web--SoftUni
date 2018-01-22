namespace Application.Controllers
{
    using System;
    using System.Linq;
    using Models.Submissions;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Contracts;

    public class SubmissionsController : BaseController
    {
        private readonly IContestService contests;
        private readonly ISubmissionService submissions;

        public SubmissionsController(IContestService contests, ISubmissionService submissions)
        {
            this.contests = contests;
            this.submissions = submissions;
        }

        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            var contests = this.contests
                .AllInSubmussion()
                .Select(c => $@"<a class=""list-group-item list-group-item-action list-group-item-dark"" data-toggle=""list"" href=""/submissions/all?contest={c.Id}"" role=""tab"">{c.Name}</a>")
                .ToList();

            this.ViewModel["contests"] = contests.Any() ? string.Join(string.Empty, contests) : string.Empty;

            string search = null;
            if (this.Request.UrlParameters.ContainsKey("contest"))
            {
                search = this.Request.UrlParameters["search"];
            }

            if (search == null)
            {
                this.ViewModel["submissions"] = string.Empty;
            }
            else
            {
               //show submissions
            }

            return this.View();
        }

        public IActionResult Create()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            var contests = this.contests
                .AllInSubmussion()
                .Select(c => $@" <option value=""{c.Id}"">{c.Name}</option>")
                .ToList();

            this.ViewModel["contestsOptions"] = string.Join(string.Empty, contests);


            return this.View();

        }

        [HttpPost]
        public IActionResult Create(CreateSubmissionModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            var rnd = new Random();
            var randomNum = rnd.Next(1, 100);

            bool succeeded = randomNum <= 70 ? false : true;
            
            this.submissions.Create(model.Code,
                model.ContestId,
                this.Profile.Id,
                succeeded);

            return this.Redirect("/submissions/all");

        }
    }
}

////<div class="tab-pane fade show active" id="first-contest" role="tabpanel">
////<ul class="list-group">
////    <li class="list-group-item list-group-item-success">Build Success</li>
////<li class="list-group-item list-group-item-success">Build Success</li>
////<li class="list-group-item list-group-item-success">Build Success</li>
////</ul>
////</div>