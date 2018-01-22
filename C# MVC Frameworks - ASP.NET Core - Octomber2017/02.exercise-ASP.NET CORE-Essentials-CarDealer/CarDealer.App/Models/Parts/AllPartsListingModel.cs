namespace CarDealer.App.Models.Parts
{
    using System.Collections.Generic;
    using Services.Models.Parts;

    public class AllPartsListingModel
    {
        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int NextPage
            => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;

        public IEnumerable<PartListingModel> Parts { get; set; }
    }
}
