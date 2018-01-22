namespace WebServer.Application.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<ProductOrder> Products { get; set; } = new List<ProductOrder>();
    }
}
