namespace RoasterOps.Models;

public enum OrderStatus { Pending, Fulfilled }

public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<OrderLineItem>? LineItems { get; set; }
}
