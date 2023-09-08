using Microsoft.EntityFrameworkCore;

namespace order_service.Migrations;

public class OrderItem
{
    public int Id { set; get; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    [Precision(10, 2)]
    public decimal Price { get; set; }
    public Order Order { get; set; } = null!;
}