namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Admin;
    using LearningSystem.Web.Areas.Admin.Models.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : AdminBaseController
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

        public async Task<IActionResult> Create()
        {
            return View(new CreateCourseViewModel
            {
                Trainers = await this.GetTreinersItems()
            });

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model, string trainer)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(trainer))
            {
                model.Trainers = await this.GetTreinersItems();
                return View(model);
            }

            this.courses.Create(
                model.Name,
                model.Description,
                model.StartDate,
                model.EndDate,
                trainer);

            return Redirect("/");
        }

        private async Task<IEnumerable<SelectListItem>> GetTreinersItems()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.Trainer);
            return trainers
                    .Select(t => new SelectListItem
                    {
                        Text = t.UserName,
                        Value = t.Id
                    })
                    .ToList();
        }
    }
}
