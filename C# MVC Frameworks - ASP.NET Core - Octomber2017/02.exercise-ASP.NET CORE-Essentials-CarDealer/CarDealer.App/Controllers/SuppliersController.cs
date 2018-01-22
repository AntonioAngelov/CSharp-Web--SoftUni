namespace CarDealer.App.Controllers
{
    using System;
    using Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services;
    using Services.Models.Suppliers;

    public class SuppliersController : BaseController
    {
        private const string SuppliersView = "Suppliers";
        private const int ListingPageSize = 25;

        private readonly ISupplierService suppliers;
        private readonly IPartService parts;

        public SuppliersController(ISupplierService suppliers, IPartService parts, ILogService logger, IHttpContextAccessor httpContextAccessor)
            :base(logger, httpContextAccessor)
        {
            this.suppliers = suppliers;
            this.parts = parts;
        }

        public IActionResult All(int page = 1)
        {
            var suppliers = this.suppliers.AllForListing(page, ListingPageSize);

            return this.View(new AllSuppliersListingModel
            {
                CurrentPage = page,
                Suppliers = suppliers,
                TotalPages = (int)Math.Ceiling(this.suppliers.Total() / (double)ListingPageSize),

            });
        }

        public IActionResult Local()
            => this.View(SuppliersView, this.GetSuppliersModel(false));

        public IActionResult Importers()
            => this.View(SuppliersView, this.GetSuppliersModel(true));

        private SuppliersModel GetSuppliersModel(bool importer)
        {
            var type = importer
                ? "Importer"
                : "Local";

            var suppliers = this.suppliers
                .AllByFilter(importer);

            return new SuppliersModel
            {
                Type = type,
                Supplers = suppliers
            };
        }

        [Authorize]
        public IActionResult Create()
            => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Create(SupplierFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.suppliers
                .Create(model.Name, model.IsImporter);

            this.logger.Log(base.GetCurrentUserId(), LogOperation.Add, "Supplier", DateTime.UtcNow);

            return RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var supplier = this.suppliers
                .Find(id);

            if (supplier == null)
            {
                return this.NotFound();
            }

            return this.View(supplier);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, SupplierFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.suppliers.Exists(id))
            {
                return this.NotFound();
            }

            this.suppliers
                .Edit(id, model.Name, model.IsImporter);

            this.logger.Log(base.GetCurrentUserId(), LogOperation.Edit, "Supplier", DateTime.UtcNow);

            return this.RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            if (!this.suppliers.Exists(id))
            {
                return this.NotFound();
            }

            var supplier = this.suppliers.Find(id);

            return this.View(new SupplierDeleteModel
            {
                Supplier = supplier,
                SupplierId = id
            });
        }


        [Authorize]
        public IActionResult Confirm(int id)
        {
            if (!this.suppliers.Exists(id))
            {
                return this.NotFound();
            }

            this.parts.RemoveSuppliersParts(id);

            this.suppliers
                .Remove(id);

            this.logger.Log(base.GetCurrentUserId(), LogOperation.Delete, "Supplier", DateTime.UtcNow);

            return RedirectToAction(nameof(this.All));
        }

    }
}
