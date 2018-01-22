namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Models.Parts;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public int Total()
            => this.db.Parts.Count();

        public IEnumerable<PartListingModel> All(int pageNumber, int pageSize = 10)
            => this.db
                .Parts
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Supplier = p.Supplier.Name
                })
                .ToList();

        public void Create(string name, decimal price, int quantity, int supplierId)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                SupplierId = supplierId
            };

            this.db.AddRange(part);

            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db.Parts.Any(p => p.Id == id);

        public PartListingModel Find(int id)
            => this.db
                .Parts
                .Where(p => p.Id == id)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Supplier = p.Supplier.Name
                })
                .FirstOrDefault();

        public void Delete(int id)
        {
            var part = this.db
                .Parts
                .Find(id);


            this.db
                .Parts
                .Remove(part);

            this.db.SaveChanges();
        }

        public void Edit(int id, int quantity, decimal price)
        {
            var part = this.db
                .Parts
                .Find(id);

            part.Quantity = quantity;
            part.Price = price;

            this.db.SaveChanges();
        }

        public PartEditModel GetForEdit(int id)
            => this.db
                .Parts
                .Where(p => p.Id == id)
                .Select(p => new PartEditModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .FirstOrDefault();

        public IEnumerable<PartDropdownModel> GetAllForDropdown()
            => this.db
                .Parts
                .Select(p => new PartDropdownModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

        public void RemoveSuppliersParts(int supplierId)
        {
            var partsToDelete = this.db
                .Parts
                .Where(p => p.SupplierId == supplierId)
                .ToList();

            this.db
                .Parts.RemoveRange(partsToDelete);

            this.db.SaveChanges();
        }
    }
}
