namespace _05.ShopHierarchy.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ItemsOrders> Items { get; set; } = new List<ItemsOrders>();
    }
}
