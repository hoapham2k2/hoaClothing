using identity_service.Models;

namespace identity_service.Repositories.JwtRepository;

public interface IJwtRepository
{ 
    //Generate JWT Token
    string GenerateJWTToken(AppUser user);
    
}