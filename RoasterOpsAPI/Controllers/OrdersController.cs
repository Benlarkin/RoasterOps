using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoasterOps.Data;
using RoasterOps.Models;

namespace RoasterOps.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.LineItems)
            .ThenInclude(li => li.Product)
            .ToListAsync();
    }

    // GET: api/orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.LineItems)
            .ThenInclude(li => li.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
            return NotFound();

        return order;
    }

    // GET: api/orders/with-details
    [HttpGet("with-details")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersWithDetails()
    {
        // pull every line‐item with its parent order and product
        var lineItems = await _context.OrderLineItems
            .Include(li => li.Order)
                .ThenInclude(o => o.LineItems)
                    .ThenInclude(li => li.Product)
            .ToListAsync();

        // group them back into orders
        var orders = lineItems
            .GroupBy(li => li.Order)
            .Select(g =>
            {
                var ord = g.Key;
                ord.LineItems = g.ToList();
                return ord;
            })
            .ToList();

        return orders;
    }


    // POST: api/orders
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        // Ensure OrderDate and Status have default values if not set
        if (order.OrderDate == default)
            order.OrderDate = DateTime.UtcNow;
        if (order.Status == 0)
            order.Status = OrderStatus.Pending;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }

    // PUT: api/orders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order updatedOrder)
    {
        if (id != updatedOrder.OrderId)
            return BadRequest();

        var existingOrder = await _context.Orders
            .Include(o => o.LineItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (existingOrder == null)
            return NotFound();

        existingOrder.CustomerName = updatedOrder.CustomerName;
        existingOrder.OrderDate = updatedOrder.OrderDate;
        existingOrder.Status = updatedOrder.Status;

        // Replace line items
        _context.OrderLineItems.RemoveRange(existingOrder.LineItems);
        existingOrder.LineItems = updatedOrder.LineItems;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/orders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.LineItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
            return NotFound();

        _context.OrderLineItems.RemoveRange(order.LineItems);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
