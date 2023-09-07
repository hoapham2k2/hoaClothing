using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using identity_service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace identity_service.Repositories.JwtRepository;

public class JwtRepository : IJwtRepository
{
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<AppUser> _userManager;
    
    public JwtRepository(SymmetricSecurityKey key, UserManager<AppUser> userManager)
    {
        _key = key;
        _userManager = userManager;
    }
    
    public string GenerateJWTToken(AppUser user)
    {
        
        //create claims (id, username, email, roles) 
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };
        
        //get roles and add them to claims
        var roles = _userManager.GetRolesAsync(user).Result;
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        //create credentials
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        //create token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7), //token expires in 7 days
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}