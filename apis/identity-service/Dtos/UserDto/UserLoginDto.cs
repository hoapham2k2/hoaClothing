using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace identity_service.Dtos.UserDto;

public class UserLoginDto
{
  public string? UserName { get; set; } 
  public string? Email { get; set; }
  [Required]
  public string? Password { get; set; }
}