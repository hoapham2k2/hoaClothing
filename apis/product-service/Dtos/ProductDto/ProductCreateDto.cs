using System.ComponentModel.DataAnnotations;
using product_service.Model;

namespace product_service.Dtos;

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public double? OldPrice { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public double Price { get; set; }
    
    [Required]
    public List<ImageUri> Images { get; set; } = new List<ImageUri>();
    
    [Required]
    public int CategoryId { get; set; }
}