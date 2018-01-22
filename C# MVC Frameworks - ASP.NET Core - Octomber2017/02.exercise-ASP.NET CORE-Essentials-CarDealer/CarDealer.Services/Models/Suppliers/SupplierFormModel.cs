namespace CarDealer.Services.Models.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    public class SupplierFormModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Importer")]
        public bool IsImporter { get; set; }
    }
}
