using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cart_service.Model;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CartId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    [Precision(10, 2)]    
    public decimal Price { get; set; }
    
    public Cart Cart { get; set; } = null!;
    
    
}