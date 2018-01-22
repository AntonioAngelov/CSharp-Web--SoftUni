namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Services;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly ICameraService cameras;
        private readonly IUserService users;

        public UsersController(
            ICameraService cameras,
            IUserService users)
        {
            this.cameras = cameras;
            this.users = users;
        }

        public IActionResult Details(string id)
        {
            return View(this.users.ById(id));
        }
    }
}
