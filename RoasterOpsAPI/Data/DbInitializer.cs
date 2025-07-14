using RoasterOps.Models;

namespace RoasterOps.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Products.Any())
            return; // DB already seeded

        var products = new List<Product>
        {
            new Product { Name = "French Roast Decaf", Type = ProductType.Green, Unit = UnitType.Bag, StockLevel = 12 },
            new Product { Name = "Midnight Sun", Type = ProductType.Roasted, Unit = UnitType.Lbs, StockLevel = 88 },
            new Product { Name = "Black Silk", Type = ProductType.Roasted, Unit = UnitType.Lbs, StockLevel = 45 },
            new Product { Name = "Organic Mexican", Type = ProductType.Green, Unit = UnitType.Bag, StockLevel = 6 }
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        var rng  = new Random();
        var year = DateTime.UtcNow.Year;
        var orders = new List<Order>();

        // seed 100 orders in Q1, 200 in Q2, 300 in Q3, 400 in Q4
        var quarterSizes = new[] { 323, 300, 444, 375 };

        for (int q = 1; q <= 4; q++)
        {
            int orderCount = quarterSizes[q - 1];
            int startMonth = (q - 1) * 3 + 1;
            int endMonth   = startMonth + 2;

            for (int i = 0; i < orderCount; i++)
            {
                int month = rng.Next(startMonth, endMonth + 1);
                int day   = rng.Next(1, DateTime.DaysInMonth(year, month) + 1);

                var order = new Order
                {
                    CustomerName = $"Customer {rng.Next(1, 50)}",
                    OrderDate = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc),
                    Status       = OrderStatus.Fulfilled,
                    LineItems    = new List<OrderLineItem>()
                };

                int itemCount = rng.Next(1, 4);
                for (int j = 0; j < itemCount; j++)
                {
                    var product = products[rng.Next(products.Count)];
                    order.LineItems.Add(new OrderLineItem
                    {
                        ProductId = product.ProductId,
                        Quantity  = rng.Next(1, 20) // quantities in the hundreds
                    });
                }

                orders.Add(order);
            }
        }

        context.Orders.AddRange(orders);
        context.SaveChanges();
    }
}
