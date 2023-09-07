using System.ComponentModel.DataAnnotations;

namespace product_service.Model;

public class Category
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9]*$")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

}