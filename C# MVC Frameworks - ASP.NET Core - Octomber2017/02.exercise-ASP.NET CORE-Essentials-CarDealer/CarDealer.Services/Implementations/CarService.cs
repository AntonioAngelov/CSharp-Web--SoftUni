namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Models.Cars;
    using Models.Parts;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> GetByMake(string make)
            => this.db
                .Cars
                .Where(c => c.Make.ToLower() == make)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

        public IEnumerable<CarWithPartsModel> GetWithParts(int pageNumber, int pageSize)
            => this.db.Cars
                .OrderByDescending(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts
                        .Select(p => new PartModel
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .ToList()
                });

        public void Create(string make, string model, long travelledDistance, IEnumerable<int> partsIds)
        {
            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            foreach (var partid in partsIds)
            {
                car.Parts.Add(new PartCar
                {
                    PartId = partid
                });
            }

            this.db
                .Add(car);

            this.db.SaveChanges();
        }

        public int Total()
            => this.db
                .Cars
                .Count();

        public IEnumerable<CarDropdownModel> All()
            => this.db
                .Cars
            .OrderByDescending(c => c.Id)
                .Select(c => new CarDropdownModel
                {
                    Id = c.Id,
                    Name = $"{c.Make} {c.Model}"
                })
               .ToList();

        public decimal CarPrice(int id)
            => this.db
                .Cars
                .Where(c => c.Id == id)
                .Select(c => c.Parts
                        .Sum(p => p.Part.Price))
                .FirstOrDefault();

        public string GetName(int id)
            => this.db
                .Cars
                .Where(c => c.Id == id)
                .Select(c => $"{c.Make} {c.Model}")
                .FirstOrDefault();

        public bool Exists(int id)
            => this.db
                .Cars
                .Any(c => c.Id == id);
    }
}
