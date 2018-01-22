namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models.Parts;

    public interface IPartService
    {
        int Total();

        IEnumerable<PartListingModel> All(int pageNumber, int pageSize);

        void Create(string name, decimal price, int quantity, int supplierId);

        bool Exists(int id);

        PartListingModel Find(int id);

        void Delete(int id);

        void Edit(int id, int quantity, decimal price);

        PartEditModel GetForEdit(int id);
        
        IEnumerable<PartDropdownModel> GetAllForDropdown();

        void RemoveSuppliersParts(int supplierId);
    }
}
