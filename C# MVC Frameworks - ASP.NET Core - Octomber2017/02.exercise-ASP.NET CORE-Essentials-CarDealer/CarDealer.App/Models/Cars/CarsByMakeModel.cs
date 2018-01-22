namespace CarDealer.App.Models.Cars
{
    using System.Collections.Generic;
    using Services.Models.Cars;

    public class CarsByMakeModel
    {
        public string Make { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }
    }
}
