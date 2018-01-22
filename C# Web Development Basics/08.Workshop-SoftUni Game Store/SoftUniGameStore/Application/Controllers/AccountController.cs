namespace SoftUniGameStore.Application.Controllers
{
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Services;
    using Services.Contracts;
    using ViewModels;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private const string RegisterView = @"account\register";
        private const string LoginView = @"account\login";

        private readonly IUserService users;

        public AccountController(IHttpRequest request) 
            : base(request)
        {
            this.users = new UserService();
        }

        public IHttpResponse Register()
            => this.FileViewResponse(RegisterView);

        public IHttpResponse Register(ReisterViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.Register();
            }

            var success = this.users
                .Create(model.Email, model.Name, model.Password);

            if (!success)
            {
                this.ShowError("E-mail is taken.");
                return this.Register();
            }
            else
            {
                this.LoginUser(model.Email);

                return this.RedirectResponse(Controller.HomePath);
            }
        }

        public IHttpResponse   Login()
        {
            return this.FileViewResponse(LoginView);
        }

        public IHttpResponse Login(LoginViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.Login();
            }

            var success = this.users.Find(model.Email, model.Password);

            if (!success)
            {
                this.ShowError("Invalid user details.");

                return this.Login();
            }
            else
            {
                this.LoginUser(model.Email);

                return RedirectResponse(HomePath);
            }
        }

        private void LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
            this.Request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return this.RedirectResponse(HomePath);
        }
    }
}
