namespace CarDealer.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual List<Part> Parts { get; set; } = new List<Part>();
    }
}
