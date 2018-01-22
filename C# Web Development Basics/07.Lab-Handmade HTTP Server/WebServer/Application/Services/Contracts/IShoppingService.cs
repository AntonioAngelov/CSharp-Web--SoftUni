namespace WebServer.Application.Services.Contracts
{
    using System.Collections.Generic;

    public interface IShoppingService
    {
        void CreateOrder(int ownerID, IEnumerable<int> productIds);
    }
}
