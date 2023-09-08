using cart_service.Model;
using Microsoft.EntityFrameworkCore;

namespace cart_service.Data;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    
}