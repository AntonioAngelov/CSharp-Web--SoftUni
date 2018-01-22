namespace SoftUniGameStore.Application.Infrastructure
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Common;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Server.Enums;
    using Server.Http;
    using Services;
    using Services.Contracts;

    public abstract class Controller
    {
        private const string AppDirectory = "Application";
        protected const string HomePath = "/home/all";

        private const string DefaultPath = @"{0}\Resources\{1}.html";
        private const string ContentPlaceholder = "{{{content}}}";

        private readonly IUserService users;


        protected Controller(IHttpRequest request)
        {
            this.ViewData = new Dictionary<string, string>
            {
                ["showError"] = "none"
            };

            this.Authentication = new Authentication(false, false);

            this.Request = request;

            this.users = new UserService();

            this.ApplyAuthentication();
        }

        protected Authentication Authentication { get; private set; }

        protected IHttpRequest Request { get; private set; }

        protected IDictionary<string, string> ViewData { get; private set; }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        protected IHttpResponse RedirectResponse(string route)
            => new RedirectResponse(route);

        protected void ShowError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        protected bool ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        this.ShowError(result.ErrorMessage);
                        return false;
                    }
                }
            }

            return true;
        }

        private string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, Controller.AppDirectory, "layout"));

            var fileHtml = File
                .ReadAllText(string.Format(DefaultPath, Controller.AppDirectory, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }

        private void ApplyAuthentication()
        {
            var anonymousDisplay = "flex";
            var authDisplay = "none";
            var adminDisplay = "none";

            var authenticatedUserEmail = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (authenticatedUserEmail != null)
            {
                anonymousDisplay = "none";
                authDisplay = "flex";

                var isAdmin = this.users.IsAdmin(authenticatedUserEmail);

                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["anonymousDisplay"] = anonymousDisplay;
            this.ViewData["authDisplay"] = authDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
        }
    }
}
