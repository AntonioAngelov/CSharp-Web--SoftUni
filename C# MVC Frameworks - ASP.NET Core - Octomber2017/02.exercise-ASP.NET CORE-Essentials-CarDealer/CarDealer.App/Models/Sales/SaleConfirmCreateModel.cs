namespace CarDealer.App.Models.Sales
{
    using System.ComponentModel.DataAnnotations;

    public class SaleConfirmCreateModel
    {
        public int CarId { get; set; }

        public int CustomerId { get; set; }

        public bool CustomerIsYoungDriver { get; set; }

        [Display(Name = "Car")]
        public string CarName { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        
        public double Discount { get; set; }

        [Display(Name = "Car Price")]
        public decimal Price { get; set; }

        [Display(Name = "Final Car Price")]
        public decimal PriceWithDiscount
        {
            get
            {
                var discount = this.Price * ((decimal) this.Discount / 100 + (this.CustomerIsYoungDriver ? 0.05m : 0));

                return this.Price - discount;
            }
        }
    }
}
