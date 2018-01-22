using System.Collections.Generic;

namespace CarDealer.Services
{
    using Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> AllByFilter(bool importer);

        IEnumerable<SupplierModel> All();

        IEnumerable<SupplierListingModel> AllForListing(int pageNumber, int pageSize);

        int Total();

        void Create(string name, bool isImporter);

        SupplierFormModel Find(int id);

        bool Exists(int id);

        void Edit(int id, string name, bool isImporter);

        void Remove(int id);
    }
}
