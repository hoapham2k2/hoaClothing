using System.ComponentModel.DataAnnotations;
using product_service.Model;

namespace product_service.Dtos;

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int CategoryId { get; set; }
}