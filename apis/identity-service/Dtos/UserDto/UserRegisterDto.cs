using Microsoft.Build.Framework;

namespace identity_service.Dtos.UserDto;

public class UserRegisterDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}