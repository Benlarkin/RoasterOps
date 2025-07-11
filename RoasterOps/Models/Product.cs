namespace RoasterOps.Models;

public enum ProductType { Green, Roasted }
public enum UnitType { Bag, Lbs }

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public UnitType Unit { get; set; }
    public decimal StockLevel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
