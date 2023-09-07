using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace product_service.Model;

public class ImageUri
{
    [Key]
    public int Id { get; set; }
    public string Uri { get; set; } = null!;
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; } = null!;
    
}