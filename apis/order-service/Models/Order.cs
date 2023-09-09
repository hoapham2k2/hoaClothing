using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace order_service.Migrations;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Precision(10, 2)] 
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;  
    public int UserId { get; set; } 
    public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long UpdatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
}