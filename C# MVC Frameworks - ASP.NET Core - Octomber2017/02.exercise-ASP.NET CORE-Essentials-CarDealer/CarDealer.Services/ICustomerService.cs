namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Customers;

    public interface ICustomerService
    {
        void Create(string name, DateTime birthDate, bool isYoungDriver);

        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerWithSalesModel CustomerTotalSales(int customerId);

        EditCustomerModel Find(int id);

        void Edit(int id, string name, DateTime birthDate);

        bool Exists(int id);

        IEnumerable<CustomerDropdownModel> All();

        string GetName(int id);

        bool IsYoungDriver(int id);
    }
}
