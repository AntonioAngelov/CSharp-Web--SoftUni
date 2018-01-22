namespace CameraBazaar.Web.Controllers
{
    using AutoMapper;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services;
    using CameraBazaar.Web.Models.Cameras;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CamerasController : Controller
    {
        private readonly UserManager<User> userManger;
        private readonly ICameraService cameras;
        private readonly IMapper mapper;

        public CamerasController(
            UserManager<User> userManger,
            ICameraService cameras,
            IMapper mapper)
        {
            this.userManger = userManger;
            this.cameras = cameras;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(CameraFormModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this.cameras.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMeterings,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this.userManger.GetUserId(User));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult All()
        {
            return View(this.cameras.AllListing());
        }

        public IActionResult Details(int id)
        {
            var currentCamera = this.cameras.ById(id);

            if (currentCamera == null)
            {
                return NotFound();
            }

            return View(currentCamera);
        }

        //DONT LIST Lightening Meters
        [Authorize]
        public IActionResult Edit(int id)
        {
            var isEditable = this.cameras.Exists(id, this.userManger.GetUserId(User));

            if (!isEditable)
            {
                return NotFound();
            }

            var camera = this.cameras.ById(id);

            var cameraFormModel = this.mapper.Map<CameraFormModel>(camera);

            return View(cameraFormModel);
        }

        //TODO
        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CameraFormModel cameraModel)
        {
            return RedirectToAction(nameof(Details), new { id = id });
        }

        public IActionResult Delete(int id)
        {
            var isDeletable = this.cameras.Exists(id, this.userManger.GetUserId(User));

            if (!isDeletable)
            {
                return NotFound();
            }

            var camera = this.cameras.ById(id);
            var cameraFormModel = this.mapper.Map<CameraFormModel>(camera);

            return View(cameraFormModel);
        }

        [HttpPost]
        public IActionResult Delete(int id, string empty)
        {
            this.cameras.Delete(id, this.userManger.GetUserId(User));

            return RedirectToAction(nameof(All));
        }
    }
}
