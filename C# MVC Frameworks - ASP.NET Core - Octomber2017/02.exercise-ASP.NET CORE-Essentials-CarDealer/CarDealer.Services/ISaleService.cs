namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models.Sales;

    public interface ISaleService
    {
        IEnumerable<SaleListingModel> All();

        SaleWithDetailsModel Details(int saleId);

        IEnumerable<SaleListingModel> Discounted(double discount);

        void Create(int customerId, int carId, double discount);
    }
}
