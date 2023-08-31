using System.ComponentModel.DataAnnotations;

namespace product_service.Model;

public class Category
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

}