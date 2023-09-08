using order_service.Migrations;

namespace order_service.Dtos.OrderDto;

public class OrderReadDto
{
    public int Id { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal TotalPrice { get; set; }
}

