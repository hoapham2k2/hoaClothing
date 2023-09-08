using Microsoft.EntityFrameworkCore;
using product_service.Model;

namespace product_service.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    
    public DbSet<ImageUri> ImageUris { get; set; } = null!;
    
    
}
