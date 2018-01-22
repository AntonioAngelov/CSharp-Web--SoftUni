namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Models.Cars;
    using Models.Sales;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleListingModel> All()
            => this.db.Sales
                .Select(s => new SaleListingModel
                {
                    Id = s.Id,
                    Discount = s.Discount,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();

        public SaleWithDetailsModel Details(int saleId)
            => this.db.Sales
                .Where(s => s.Id == saleId)
                .Select(s => new SaleWithDetailsModel
                {
                    Discount = s.Discount,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    CustomerName = s.Customer.Name,
                    CarDetails = new CarModel
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    }
                })
                .FirstOrDefault();

        public IEnumerable<SaleListingModel> Discounted(double discount)
        {
            var salesQuery = this.db.Sales
                .Where(s => !s.Discount.Equals(0) || s.Customer.IsYoungDriver)
                .AsQueryable();

            if (!discount.Equals(0))
            {
                salesQuery = salesQuery
                    .Where(s => s.Discount.Equals(discount));
            }

            return salesQuery
                .Select(s => new SaleListingModel
                {
                    Id = s.Id,
                    Discount = s.Discount,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();
        }

        public void Create(int customerId, int carId, double discount)
        {
            var sale = new Sale
            {
                CarId = carId,
                CustomerId = customerId,
                Discount = discount
            };

            this.db.Add(sale);
            this.db.SaveChanges();
        }
    }
}
