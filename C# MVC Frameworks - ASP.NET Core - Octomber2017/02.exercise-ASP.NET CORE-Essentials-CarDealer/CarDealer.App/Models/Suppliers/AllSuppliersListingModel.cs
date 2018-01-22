namespace CarDealer.App.Models.Suppliers
{
    using System.Collections.Generic;
    using Services.Models.Suppliers;

    public class AllSuppliersListingModel
    {
        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int NextPage
            => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;

        public IEnumerable<SupplierListingModel> Suppliers { get; set; }
    }
}
