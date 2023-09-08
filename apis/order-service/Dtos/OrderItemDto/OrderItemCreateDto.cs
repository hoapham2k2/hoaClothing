namespace order_service.Dtos.OrderItemDto;

public class OrderItemCreateDto
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}