namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    using Services;
    using Services.Models;
    using Services.Models.Customers;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route(nameof(Create))]
        public IActionResult Create()
            => this.View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.customers
                .Create(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(nameof(this.All), new {order = OrderDirection.Ascending});
        }

        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customers
                .Find(id);

            if (customer == null)
            {
                return this.NotFound();
            }

            return this.View(customer);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, EditCustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.customers.Exists(id))
            {
                return this.NotFound();
            }

            this.customers
                .Edit(id, model.Name, model.BirthDate);

            return RedirectToAction(nameof(this.All), new { order = OrderDirection.Ascending });
        }

        [Route("all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var customers = this.customers
                .OrderedCustomers(orderDirection);

            return this.View(new OrderedCustomersModel
            {
                OrderDirection = orderDirection,
                Customers = customers
            });
        }

        [Route("{id}")]
        public IActionResult CustomerTotalSales(int id)
        {
            var customer = this.customers
                .CustomerTotalSales(id);

            if (customer != null)
            {
                return this.View(customer);
            }
            else
            {
                return this.NotFound();
            }

        }
    }

}
