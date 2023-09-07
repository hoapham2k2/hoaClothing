using System.ComponentModel.DataAnnotations;
using product_service.Model;

namespace product_service.Dtos;

public class ProductUpdateDto
{
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal? OldPrice { get; set; }
    
    [Microsoft.Build.Framework.Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Microsoft.Build.Framework.Required]
   public List<ImageUri> Images { get; set; } = new List<ImageUri>();
    
    [Microsoft.Build.Framework.Required]
    public int CategoryId { get; set; }
}