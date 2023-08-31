using System.ComponentModel.DataAnnotations;

namespace product_service.Dtos;

public class CategoryCreateDto
{
    [Required]
    public string Name { get; set; }

}