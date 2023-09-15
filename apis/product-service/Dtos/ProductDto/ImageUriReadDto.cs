using System.ComponentModel.DataAnnotations;

namespace product_service.Dtos;

public class ImageUriReadDto
{
    [Key]
    public int Id { get; set; }
    public string Uri { get; set; } = null!;
}