using System.ComponentModel.DataAnnotations;
using product_service.Model;

namespace product_service.Dtos;

public class ProductReadDto
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal Price { get; set; }
    public List<ImageUri> Images { get; set; } = new List<ImageUri>();
    [Required]
    public int CategoryId { get; set; }
}