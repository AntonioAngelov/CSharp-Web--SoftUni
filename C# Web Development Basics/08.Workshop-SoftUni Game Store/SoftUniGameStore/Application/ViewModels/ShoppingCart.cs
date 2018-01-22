namespace SoftUniGameStore.Application.ViewModels
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "%^Current_Shopping_Cart^%";

        public List<int> GamesIds { get; private set; } = new List<int>();
    }
}
