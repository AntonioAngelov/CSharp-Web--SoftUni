namespace CarDealer.App.Models.Sales
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SaleCreateModel
    {
        [Display(Name = "Car")]
        public int SeLectedCar { get; set; }
        
        public IEnumerable<SelectListItem> Cars { get; set; }

        [Display(Name = "Customer")]
        public int SeLectedCustomer { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        [Range(0, 100)]
        public double Discount { get; set; }
    }
}
