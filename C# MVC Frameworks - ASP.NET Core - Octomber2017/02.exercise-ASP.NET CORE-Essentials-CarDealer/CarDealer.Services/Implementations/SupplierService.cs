namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Models.Suppliers;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> AllByFilter(bool importer)
            => this.db.Suppliers
                .Where(s => s.IsImporter == importer)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parts = s.Parts.Count
                })
                .ToList();

        public IEnumerable<SupplierModel> All()
            => this.db.Suppliers
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parts = s.Parts.Count
                })
                .ToList();

        public IEnumerable<SupplierListingModel> AllForListing(int pageNumber, int pageSize = 10)
            => this.db
                .Suppliers
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SupplierListingModel
                {
                    Id = s.Id,
                    IsImporter = s.IsImporter,
                    Name = s.Name
                })
                .ToList();

        public int Total()
            => this.db
                .Suppliers
                .Count();

        public void Create(string name, bool isImporter)
        {
            var supplier = new Supplier
            {
                Name = name,
                IsImporter = isImporter
            };

            this.db.Add(supplier);
            this.db.SaveChanges();
        }

        public SupplierFormModel Find(int id)
            => this.db
                .Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierFormModel
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .FirstOrDefault();

        public bool Exists(int id)
            => this.db
            .Suppliers
            .Any(s => s.Id == id);

        public void Edit(int id, string name, bool isImporter)
        {
            var supplier = this.db
                .Suppliers
                .Find(id);

            supplier.Name = name;
            supplier.IsImporter = isImporter;

            this.db.SaveChanges();
        }

        public void Remove(int id)
        {
            var supplier = this.db
                .Suppliers
                .Find(id);

            this.db
                .Suppliers
                .Remove(supplier);

            this.db.SaveChanges();
        }
    }
}
