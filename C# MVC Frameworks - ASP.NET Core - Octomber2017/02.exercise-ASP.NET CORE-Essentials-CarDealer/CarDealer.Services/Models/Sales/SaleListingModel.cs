namespace CarDealer.Services.Models.Sales
{
    public class SaleListingModel : SaleModel
    {
        public int Id { get; set; }

        public bool IsYoungDriver { get; set; }
        
        public decimal DiscountedPrice
            => this.Price * (1 - (decimal) this.Discount 
            + (this.IsYoungDriver ? 0.05m : 0));
    }
}
