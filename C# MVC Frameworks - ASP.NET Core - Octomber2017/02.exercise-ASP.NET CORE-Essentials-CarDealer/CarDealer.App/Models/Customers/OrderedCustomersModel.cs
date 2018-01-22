namespace CarDealer.App.Models.Customers
{
    using System.Collections.Generic;
    using Services.Models;
    using Services.Models.Customers;

    public class OrderedCustomersModel
    {
        public OrderDirection OrderDirection { get; set; }

        public IEnumerable<CustomerModel> Customers { get; set; }
    }
}
