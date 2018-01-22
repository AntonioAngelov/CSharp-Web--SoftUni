namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public CoursesController(
            ICourseService courses,
            UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
            => View(this.courses.ById(id));

        public IActionResult SignUp(int id, string username)
        {
            this.courses.SignUp(id, username);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        //TODO Done remove user from course
        public IActionResult SignOut(int id, string username)
        {
            this.courses.SignOut(id, username);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        
    }
}
