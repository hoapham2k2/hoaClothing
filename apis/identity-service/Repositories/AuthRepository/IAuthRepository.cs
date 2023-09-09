using identity_service.Dtos.UserDto;
using identity_service.Models;

namespace identity_service.Repositories.AuthRepository;

public interface IAuthRepository
{
   Task<ServiceResponse<string>> Register(UserRegisterDto userRegisterDto); //string because we will return a token
   Task<ServiceResponse<string>> Login(UserLoginDto userLoginDto); //string because we will return a token 

   Task<bool> CheckUserExist(int userId);
}