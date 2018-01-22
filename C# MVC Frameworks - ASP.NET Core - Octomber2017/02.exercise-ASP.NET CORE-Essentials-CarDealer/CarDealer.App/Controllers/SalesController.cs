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
    using Models.Sales;
    using Newtonsoft.Json;
    using Services;

    public class SalesController : BaseController
    {
        private readonly ISaleService sales;
        private readonly ICustomerService customers;
        private readonly ICarService cars;
        

        public SalesController(ISaleService sales, ICustomerService customers, ICarService cars, ILogService logger, IHttpContextAccessor httpContextAccessor)
            :base(logger, httpContextAccessor)
        {
            this.sales = sales;
            this.customers = customers;
            this.cars = cars;
        }

        [Authorize]
        [Route("sales/create", Order = 1)]
        public IActionResult Create()
        => this.View(new SaleCreateModel
        {
            Customers = this.GetCustomersForDropown(),
            Cars = this.GetCarsForDropown()
        });


        [Authorize]
        [HttpPost]
        [Route("sales/create", Order = 1)]
        public IActionResult Create(SaleCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Customers = this.GetCustomersForDropown();
                model.Cars = this.GetCarsForDropown();

                return this.View(model);
            }

            if (!this.cars.Exists(model.SeLectedCar) || !this.customers.Exists(model.SeLectedCustomer))
            {
                return RedirectToAction(nameof(All));
            }

            TempData["SaleInfo"] = JsonConvert.SerializeObject(new SaleConfirmCreateModel
            {
                CarId = model.SeLectedCar,
                CarName = this.cars
                    .GetName(model.SeLectedCar),
                CustomerId = model.SeLectedCustomer,
                CustomerName = this.customers
                    .GetName(model.SeLectedCustomer),
                Discount = model.Discount,
                CustomerIsYoungDriver = this.customers
                    .IsYoungDriver(model.SeLectedCustomer),
                Price = this.cars
                    .CarPrice(model.SeLectedCar)
            });

            return this.RedirectToAction(nameof(ConfirmCreate));
        }

        [Authorize]
        [Route("sales/confirm", Order = 2)]
        public IActionResult ConfirmCreate()
        {
            if (!TempData.ContainsKey("SaleInfo"))
            {
                return RedirectToAction(nameof(this.All));
            }
            else
            {
                return this.View(JsonConvert.DeserializeObject<SaleConfirmCreateModel>(TempData["SaleInfo"].ToString()));
            }
        }

        [Authorize]
        [HttpPost]
        [Route("sales/confirm", Order = 2)]
        public IActionResult ConfirmCreate([FromForm]SaleConfirmCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.NotFound();
            }

            this.sales.Create(model.CustomerId, model.CarId, model.Discount / 100);

            this.logger.Log(base.GetCurrentUserId(), LogOperation.Add, "Sale", DateTime.UtcNow);

            return RedirectToAction(nameof(this.All));
        }

        [Route("sales")]
        public IActionResult All()
        {
            var allSales = this.sales.All();

            return this.View(allSales);
        }

        [Route("sales/{id}", Order = 3)]
        public IActionResult Details(int id)
        {
            var sale = this.sales.Details(id);

            if (sale != null)
            {
                return this.View(sale);
            }
            else
            {
                return this.NotFound();
            }
        }

        [Route("sales/discount/{percent?}")]
        public IActionResult Discounted(int percent)
        {
            var discountedSales = this.sales
                .Discounted((double)percent / 100);

            return this.View(new DiscountedSalesModel
            {
                Discount = percent,
                Sales = discountedSales
            });
        }

        private IEnumerable<SelectListItem> GetCarsForDropown()
            => this.cars
                .All()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

        private IEnumerable<SelectListItem> GetCustomersForDropown()
            => this.customers
                .All()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

    }
}
