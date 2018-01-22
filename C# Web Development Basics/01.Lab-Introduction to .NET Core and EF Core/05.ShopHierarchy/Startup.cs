namespace _05.ShopHierarchy
{
    using System;
    using System.Linq;
    using Models;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new ShopDbContext())
            {
                InitializeDb(context);
                SaveSalesmen(context);
                SaveItems(context);
                ProcessCommnads(context);
                //PrintCustomer(context);
                //PrintSalesmenByNumberOfCustomers(context);
                //PrintCustomersWithOrdersAndReviews(context);

                //exercise 8 Explicit Data Loading
                //PrintCustomerIno(context);

                //exercise 9 Query Loaded Data
                PrintNumberOfOrdersWithMoreItems(context);

            }
        }

        private static void PrintNumberOfOrdersWithMoreItems(ShopDbContext context)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customerInfo = context.Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    OrdersCount = c.Orders
                        .Where(o => o.Items.Count > 1)
                        .Count()
                })
                .FirstOrDefault();

            Console.WriteLine($"Orders: {customerInfo.OrdersCount}");
        }

        private static void PrintCustomerIno(ShopDbContext context)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customerInfo = context.Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewsCount = c.Reviews.Count,
                    SalesmanName = c.Salesman.Name
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customerInfo.Name}");
            Console.WriteLine($"Orders count: {customerInfo.OrdersCount}");
            Console.WriteLine($"Reviews count: {customerInfo.ReviewsCount}");
            Console.WriteLine($"Salesman: {customerInfo.SalesmanName}");

        }

        private static void PrintCustomer(ShopDbContext context)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customer = context.Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    Orders = c.Orders
                        .Select(o => new
                        {
                            o.Id,
                            ItemsCount = o.Items.Count
                        })
                        .OrderBy(o => o.Id)
                        .ToList(),
                    ReviewsCount = c.Reviews.Count
                })
                .FirstOrDefault();

            foreach (var order in customer.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.ItemsCount} items");
            }

            Console.WriteLine($"reviews: {customer.ReviewsCount}");
        }

        private static void SaveItems(ShopDbContext context)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var itemData = input.Split(';');
                var itemName = itemData[0];
                var price = decimal.Parse(itemData[1]);

                context.Items.Add(new Item() {Price = price, Name = itemName});

                context.SaveChanges();
            }
        }

        private static void PrintCustomersWithOrdersAndReviews(ShopDbContext context)
        {
            var customersData = context.Customers
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewsCount = c.Reviews.Count
                })
                .OrderByDescending(c => c.OrdersCount)
                .ThenByDescending(c => c.ReviewsCount);

            foreach (var customer in customersData)
            {
                Console.WriteLine(customer.Name);
                Console.WriteLine($"Orders: {customer.OrdersCount}");
                Console.WriteLine($"Reviews: {customer.ReviewsCount}");
            }
        }

        private static void PrintSalesmenByNumberOfCustomers(ShopDbContext context)
        {
            var salesmenData = context.Salesmen
                .Select(s => new
                {
                    s.Name,
                    CustomersCount = s.Customers.Count
                })
                .OrderByDescending(s => s.CustomersCount)
                .ThenBy(s => s.Name);



            foreach (var salesman in salesmenData)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.CustomersCount} customers");
            }

        }

        private static void ProcessCommnads(ShopDbContext context)
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var parts = input.Split('-');
                var command = parts[0];



                switch (command)
                {
                    case "register":
                        RegisterCustomer(context, parts[1]);
                        break;
                    case "order":
                        RegisterOrder(context, parts[1]);
                        break;
                    case "review":
                        RegisterReview(context, parts[1]);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void RegisterReview(ShopDbContext context, string arguments)
        {
            var reviewData = arguments.Split(';');
            var customerId = int.Parse(reviewData[0]);
            var itemId = int.Parse(reviewData[1]);

            context.Reviews.Add(new Review() {CustomerId = customerId, ItemId = itemId});

            context.SaveChanges();
        }

        private static void RegisterOrder(ShopDbContext context, string arguments)
        {
            var orderData = arguments.Split(';');
            var customerId = int.Parse(orderData[0]);
            var items = orderData
                .Skip(1)
                .Select(int.Parse)
                .ToList();

            var order = new Order() { CustomerId = customerId};

            foreach (var itemId in items)
            {
               order.Items.Add( new ItemsOrders()
               {
                   ItemId = itemId
               }); 
            }

            context.Orders.Add(order);
            context.SaveChanges();
        }

        private static void RegisterCustomer(ShopDbContext context, string arguments)
        {
            var customerData = arguments.Split(';');
            var name = customerData[0];
            var salesmanId = int.Parse(customerData[1]);


            context.Customers.Add(new Customer() {Name = name, SalesmanId = salesmanId});

            context.SaveChanges();
        }

        private static void SaveSalesmen(ShopDbContext context)
        {
            var input = Console.ReadLine();
            var salesmenNames = input.Split(';');

            foreach (var name in salesmenNames)
            {
                context.Salesmen.Add(new Salesman() {Name = name});
            }

            context.SaveChanges();
        }

        private static void InitializeDb(ShopDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
