namespace CarDealer.Services.Models.Customers
{
    using System.Collections.Generic;
    using System.Linq;
    using Sales;

    public class CustomerWithSalesModel
    {
        public string Name { get; set; }

        public bool IsYoungDriver { get; set; }

        public IEnumerable<SaleModel> BoughtCars { get; set; }

        public int TotalBoughtCars => 
            this.BoughtCars.Count();

        public decimal TotalMoneySpent
        {
            get
            {
                return this.BoughtCars
                           .Sum(c => c.Price * (1 - (decimal) c.Discount))
                       * (this.IsYoungDriver ? 0.95m : 1);
            }
        }
    }
}
