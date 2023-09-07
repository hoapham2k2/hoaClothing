using AutoMapper;
using identity_service.Dtos.UserDto;
using identity_service.Models;

namespace identity_service.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Source, Destination>();
        CreateMap<UserLoginDto, AppUser >(); // UserLoginDto -> AppUser
        CreateMap<UserRegisterDto, AppUser >(); // UserRegisterDto -> AppUser
     
        
    }
    
}