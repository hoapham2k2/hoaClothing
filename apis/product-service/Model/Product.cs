using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace product_service.Model;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal Price { get; set; }
    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<ImageUri> Images { get; set; } = new List<ImageUri>();
    
    
}