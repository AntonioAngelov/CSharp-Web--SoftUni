namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Models;
    using Models.Customers;
    using Models.Sales;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            this.db
                .Customers
                .Add(new Customer
                {
                    Name = name,
                    BirthDate = birthDate,
                    IsYoungDriver = isYoungDriver
                });

            this.db.SaveChanges();
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customersQuery = this.db
                .Customers
                .AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery.OrderBy(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery.OrderByDescending(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerWithSalesModel CustomerTotalSales(int customerId)
            => this.db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new CustomerWithSalesModel
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BoughtCars = c.Sales
                    .Select(s => new SaleModel
                        {
                            Discount = s.Discount,
                            Price = s.Car.Parts.Sum(p => p.Part.Price)
                        })

                    
                })
                .FirstOrDefault();

        public EditCustomerModel Find(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new EditCustomerModel
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate
                })
                .FirstOrDefault();

        public void Edit(int id, string name, DateTime birthDate)
        {
            var customer = this.db.Customers
                .Find(id);

            customer.Name = name;
            customer.BirthDate = birthDate;

            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db
            .Customers
            .Any(c => c.Id == id);

        public IEnumerable<CustomerDropdownModel> All()
            => this.db
                .Customers
                .OrderByDescending(c => c.Id)
                .Select(c => new CustomerDropdownModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public string GetName(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();

        public bool IsYoungDriver(int id)
            => this.db.Customers
                .Find(id)
                .IsYoungDriver;
    }
}
