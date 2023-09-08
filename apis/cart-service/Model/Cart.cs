using System.ComponentModel.DataAnnotations;

namespace cart_service.Model;

public class Cart
{
    [Key]
    public int Id { get;set; }
    [Required]
    public int UserId { get; set; }
    
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    
    
}