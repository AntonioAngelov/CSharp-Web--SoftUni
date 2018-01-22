using Microsoft.AspNetCore.Mvc;

namespace CarDealer.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Parts;
    using Services;
    using Services.Models.Parts;

    public class PartsController : Controller
    {
        private const int ListingPageSize = 25;

        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult All(int page = 1)
        {
            var parts = this.parts
                .All(page, ListingPageSize);

            return this.View(new AllPartsListingModel
            {
                CurrentPage = page,
                TotalPages = (int) Math.Ceiling(this.parts.Total() / (double) ListingPageSize),
                Parts = parts
            });
        }

        public IActionResult Create()
        {
            var allSuppliers = this.GetSupplierListItems();

            return this.View(new PartCreateModel
            {
                Suppliers = allSuppliers
            });
        }

        [HttpPost]
        public IActionResult Create(PartCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Suppliers = this.GetSupplierListItems();

                return this.View(model);
            }

            this.parts
                .Create(model.Name, model.Price, model.Quantity, model.SupplierId);

            return RedirectToAction(nameof(this.All));
        }

        public IActionResult Remove(int id)
        {
            if (!this.parts.Exists(id))
            {
                return this.NotFound();
            }

            var partToDelete = this.parts
                .Find(id);

            return this.View(partToDelete);
        }
        
        public IActionResult Confirm(int id)
        {
            if (!this.parts.Exists(id))
            {
                return this.NotFound();
            }

            this.parts.
                Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        private IEnumerable<SelectListItem> GetSupplierListItems()
            => this.suppliers
                .All()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });

        public IActionResult Edit(int id)
        {
            if (!this.parts.Exists(id))
            {
                return this.NotFound();
            }

            var partForEdit = this.parts
                .GetForEdit(id);

            return this.View(partForEdit);

        }

        [HttpPost]
        public IActionResult Edit(int id, PartEditModel model)
        {
            if (!this.parts.Exists(id))
            {
                return this.NotFound();
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.parts
                .Edit(id, model.Quantity, model.Price);

            return this.RedirectToAction(nameof(this.All));

        }
    }
}