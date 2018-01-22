namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartEditModel
    {
        [Range(0, double.MaxValue, ErrorMessage = "Price must be possitive number.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be more or equal 1.")]
        public int Quantity { get; set; }

        public string Name { get; set; }
    }
}
