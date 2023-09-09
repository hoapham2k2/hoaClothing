namespace order_service.Dtos.OrderDto;

public class OrderCreateDto
{

    public int Status { get; set; }

    public decimal TotalPrice { get; set; }
    public int UserId { get; set; } 
    public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long UpdatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

}