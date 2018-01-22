namespace CarDealer.App.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerCreateModel
    {
        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [Display(Name = "Young Driver")]
        public bool IsYoungDriver { get; set; }
    }
}
