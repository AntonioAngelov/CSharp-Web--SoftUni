namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Services;
    using LearningSystem.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ICourseService courses;

        public HomeController(ICourseService courses)
        {
            this.courses = courses;
        }

        public IActionResult Index()
        {
            return View(this.courses.AllListing());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
