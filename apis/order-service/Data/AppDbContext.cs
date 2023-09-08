using Microsoft.EntityFrameworkCore;
using order_service.Migrations;

namespace order_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(10, 2);
        
        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Price)
            .HasPrecision(10, 2);
    }
}