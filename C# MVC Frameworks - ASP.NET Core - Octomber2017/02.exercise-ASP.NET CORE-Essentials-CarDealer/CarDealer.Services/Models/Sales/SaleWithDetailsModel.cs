namespace CarDealer.Services.Models.Sales
{
    using Cars;

    public class SaleWithDetailsModel : SaleListingModel
    {
        public CarModel CarDetails { get; set; }

        public string CustomerName { get; set; }
    }
}
