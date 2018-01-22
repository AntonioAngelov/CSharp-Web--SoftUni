namespace WebServer.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int ownerID, IEnumerable<int> productIds)
        {
            using (var context = new ByTheCakeDbContext())
            {
                var order = new Order
                {
                    OwnerId = ownerID,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds.Select(id => new ProductOrder
                    {
                        ProductId = id
                    })
                    .ToList()
                };

                context.Orders.Add(order);
                context.SaveChanges();

            }
        }
    }
}
