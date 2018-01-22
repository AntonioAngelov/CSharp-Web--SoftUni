﻿namespace CarDealer.App.Models.Cars
{
    using System.Collections.Generic;
    using Services.Models.Cars;

    public class CarsListingModel
    {
        public IEnumerable<CarWithPartsModel> Cars { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int NextPage
            => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;
    }
}
