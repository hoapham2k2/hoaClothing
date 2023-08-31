using System.ComponentModel.DataAnnotations;

namespace product_service.Model;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    
}