namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using LearningSystem.Web.Models.Trainers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TrainersController : Controller
    {
        private readonly ITrainerService trainers;
        private readonly UserManager<User> userManager;

        public TrainersController(ITrainerService trainers, UserManager<User> userManager)
        {
            this.trainers = trainers;
            this.userManager = userManager;
        }

        public IActionResult Courses()
        {
            var trainerId = this.userManager.GetUserId(User);
            var courses = this.trainers.Courses(trainerId);

            return View(courses);
        }

        public IActionResult Students(int id)
        {
            var userId = this.userManager.GetUserId(User);
            if (!this.trainers.IsTrainer(id, userId))
            {
                return NotFound();
            }

            var students = this.trainers.StudentsInCourse(id);

            return View(new StudentInCourseViewModel
            {
                Students = students,
                Course = this.trainers.ById(id)
            });
        }

        [HttpPost]
        public IActionResult GradeStudent(int id, string studentId, Grade grade)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);
            if (!this.trainers.IsTrainer(id, userId))
            {
                return BadRequest();
            }

            var success = this.trainers.AddGrade(id, studentId, grade);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Students), new { id });

        }
    }
}
