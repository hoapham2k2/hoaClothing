namespace product_service.Dtos;

public class ImageUriCreateDto
{
    public string Uri { get; set; } = null!;
    public int ProductId { get; set; }
}