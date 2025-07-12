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

        var order = new Order
        {
            CustomerName = "Stumptown Cafe Wholesale",
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            LineItems = new List<OrderLineItem>
            {
                new OrderLineItem
                {
                    ProductId = products[1].ProductId, 
                    Quantity = 15
                },
                new OrderLineItem
                {
                    ProductId = products[2].ProductId, 
                    Quantity = 10
                }
            }
        };

        context.Orders.Add(order);
        context.SaveChanges();
    }
}
