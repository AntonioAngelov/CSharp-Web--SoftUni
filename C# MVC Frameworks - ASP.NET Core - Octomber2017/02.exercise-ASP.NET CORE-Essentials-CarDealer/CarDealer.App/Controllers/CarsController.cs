namespace CarDealer.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Cars;
    using Services;

    [Route("cars")]
    public class CarsController : BaseController
    {
        private const int ListingPageSize = 25;

        private readonly ICarService cars;
        private readonly IPartService parts;
        
        public CarsController(ICarService cars, IPartService parts, ILogService logger, IHttpContextAccessor httpContextAccessor)
            :base(logger, httpContextAccessor)
        {
            this.cars = cars;
            this.parts = parts;
        }

        [Route("{make}", Order = 3)]
        public IActionResult ByMake(string make)
        {
            var cars = this.cars
                .GetByMake(make.ToLower());

            return this.View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

        [Route("parts", Order = 1)]
        public IActionResult Parts(int page = 1)
        {
            var allCars = this.cars
                .GetWithParts(page, ListingPageSize);

            return this.View(new CarsListingModel
            {
                Cars = allCars,
                TotalPages = (int)Math.Ceiling(this.cars.Total() / (double)ListingPageSize), 
                CurrentPage = page
            });
        }

        [Authorize]
        [Route("create", Order = 2)]
        public IActionResult Create()
        {
            var partsForDropdown = this.GetPartsForDropdown();

            return this.View(new CarCreateModel
            {
                AllParts = partsForDropdown
            });
        }
        [HttpPost]
        [Authorize]
        [Route("create")]
        public IActionResult Create(CarCreateModel carModel)
        {
            if (!ModelState.IsValid)
            {
                var partsForDropdown = this.GetPartsForDropdown();

                return this.View(new CarCreateModel
                {
                    AllParts = partsForDropdown
                });
            }

            var filteredPartsIds = carModel
                .SelectedPartsIds
                .Where(pid => this.parts.Exists(pid));

            this.cars
                .Create(carModel.Car.Make, carModel.Car.Model, carModel.Car.TravelledDistance * 1000, filteredPartsIds);

            this.logger.Log(base.GetCurrentUserId(), LogOperation.Add, "Car", DateTime.UtcNow);

            return RedirectToAction(nameof(this.Parts));
        }

        private IEnumerable<SelectListItem> GetPartsForDropdown()
            => this.parts
                .GetAllForDropdown()
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });
    }
}
