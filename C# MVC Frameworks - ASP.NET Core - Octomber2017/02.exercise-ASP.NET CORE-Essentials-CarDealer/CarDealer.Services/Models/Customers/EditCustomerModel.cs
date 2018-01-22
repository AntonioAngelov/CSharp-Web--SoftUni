namespace CarDealer.Services.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditCustomerModel
    {
        [Required]
        public string Name { get; set; }

        [Display]
        public DateTime BirthDate { get; set; }
    }
}
