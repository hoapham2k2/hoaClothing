using System.ComponentModel.DataAnnotations;

namespace product_service.Dtos;

public class CategoryReadDto
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

}