using order_service.Migrations;

namespace order_service.Dtos.OrderDto;

public class OrderReadDto
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice { get; set; }
    public int UserId { get; set; } 
    public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long UpdatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}

