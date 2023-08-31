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
    [Required]
    public int CategoryId { get; set; }
}