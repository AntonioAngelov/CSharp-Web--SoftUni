namespace CarDealer.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PartCars")]
    public class PartCar
    {
        public int PartId { get; set; }

        public Part Part { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
