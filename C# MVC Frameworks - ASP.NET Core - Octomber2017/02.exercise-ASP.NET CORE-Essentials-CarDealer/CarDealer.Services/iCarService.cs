namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models.Cars;

    public interface ICarService
    {
        IEnumerable<CarModel> GetByMake(string make);

        IEnumerable<CarWithPartsModel> GetWithParts(int pageNumber, int pageSize);

        void Create(string make, string model, long travelledDistance, IEnumerable<int> partsIds);

        int Total();

        IEnumerable<CarDropdownModel> All();

        decimal CarPrice(int id);

        string GetName(int id);

        bool Exists(int id);
    }
}
