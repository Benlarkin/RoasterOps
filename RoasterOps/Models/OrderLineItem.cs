namespace RoasterOps.Models;
using System.Text.Json.Serialization;


public class OrderLineItem
{
    public int OrderLineItemId { get; set; }

    public int OrderId { get; set; }
    [JsonIgnore] // fixes issue with cycles during API requests e.g. Order -> LineItems -> Order -> LineItems ...
    public Order? Order { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public decimal Quantity { get; set; }
}
