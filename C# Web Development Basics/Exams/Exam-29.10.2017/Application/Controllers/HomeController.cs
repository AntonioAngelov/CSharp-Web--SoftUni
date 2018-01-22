namespace Application.Controllers
{
    using SimpleMvc.Framework.Contracts;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            this.ViewModel["guestDisplay"] = "block";
            this.ViewModel["authenticated"] = "none";

            if (this.User.IsAuthenticated)
            {
                this.ViewModel["guestDisplay"] = "none";
                this.ViewModel["authenticated"] = "flex";
                this.ViewModel["userName"] = this.Profile.FullName;
            }

            return this.View();
        }
    }
}
