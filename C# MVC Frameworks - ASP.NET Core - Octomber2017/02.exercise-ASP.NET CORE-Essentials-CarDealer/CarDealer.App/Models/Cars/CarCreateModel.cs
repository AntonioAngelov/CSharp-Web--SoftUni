namespace CarDealer.App.Models.Cars
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Models.Cars;

    public class CarCreateModel
    {
        public CarModel Car { get; set; }

        public List<int> SelectedPartsIds { get; set; }

        public IEnumerable<SelectListItem> AllParts { get; set; }
    }
}
