namespace CarDealer.App.Models.Sales
{
    using System.Collections.Generic;
    using Services.Models.Sales;

    public class DiscountedSalesModel
    {
        public int Discount { get; set; }

        public IEnumerable<SaleListingModel> Sales { get; set; }
    }
}
