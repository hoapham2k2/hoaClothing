using AutoMapper;
using identity_service.Dtos.UserDto;
using identity_service.Models;
using identity_service.Repositories.JwtRepository;
using identity_service.Repositories.RoleRepository;
using Microsoft.AspNetCore.Identity;

namespace identity_service.Repositories.AuthRepository;

public class AuthRepository : IAuthRepository
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtRepository _jwtRepository;
    private readonly IRoleRepository _roleRepository;

    public AuthRepository( IConfiguration configuration, IMapper mapper, UserManager<AppUser> userManager, IJwtRepository jwtRepository, IRoleRepository roleRepository)
    {
        _configuration = configuration;
        _mapper = mapper;
        _userManager = userManager;
        _jwtRepository = jwtRepository;
        _roleRepository = roleRepository;
    }


    public async Task<ServiceResponse<string>> Register(UserRegisterDto userRegisterDto)
    {
        ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
        if (await CheckIfUserExists(username: userRegisterDto.UserName, email: userRegisterDto.Email))
        {
            serviceResponse.Success = false;
            serviceResponse.Messages = new List<string> {"User already exists"};
            return serviceResponse;
        }

        AppUser user = _mapper.Map<AppUser>(userRegisterDto);
        user.UserName = userRegisterDto.UserName;
        user.Email = userRegisterDto.Email;
        IdentityResult result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        
        //TODO: create a role for the user and add it to the user
        if (result.Succeeded)
        {
            ServiceResponse<string> roleResponse = await _roleRepository.CreateRole("User");
            if (roleResponse.Data is not null)
            {
                //Thêm role cho user vừa tạo ở bảng AspNetUserRoles
                var addRoleResult = await _userManager.AddToRoleAsync(user, "User"); 
                Console.WriteLine($"====> Add role result: {addRoleResult}");
            }
        }
        
        if (!result.Succeeded)
        {
            serviceResponse.Success = false;
            serviceResponse.Messages = result.Errors.Select(e => e.Description);
            return serviceResponse;
        }

        serviceResponse.Data = _jwtRepository.GenerateJWTToken(user);
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> Login(UserLoginDto userLoginDto)
    {
        ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
        AppUser user = await _userManager.FindByNameAsync(userLoginDto.UserName);
        if (user == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Messages = new List<string> {"User not found"};
            return serviceResponse;
        }

        bool result = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
        if (!result)
        {
            serviceResponse.Success = false;
            serviceResponse.Messages = new List<string> {"Wrong password"};
            return serviceResponse;
        }

        serviceResponse.Data = _jwtRepository.GenerateJWTToken(user);
        return serviceResponse;
    }
    
    // ------ Helper methods ------
    
    private async Task<bool> CheckIfUserExists(string? username = null, string? email = null)
    {
       switch (username, email)
       {
           case (null, null):
               throw new ArgumentNullException("Both username and email cannot be null");
           case (not null, not null):
           case (not null, null):
               return await _userManager.FindByNameAsync(username) != null;
           case (null, not null):
               return await _userManager.FindByEmailAsync(email) != null;
       }
        
    }
    
}