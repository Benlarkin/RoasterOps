namespace RoasterOps.Models;

public class OrderLineItem
{
    public int OrderLineItemId { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public decimal Quantity { get; set; }
}
