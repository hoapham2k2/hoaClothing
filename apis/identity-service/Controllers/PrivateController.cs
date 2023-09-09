using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity_service.Repositories.AuthRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace identity_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        
        public PrivateController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        //check user exist
        [HttpGet("checkUserExist/{userId}")]
        public async Task<IActionResult> CheckUserExist(int userId)
        {
            var isUserExist  = await _authRepository.CheckUserExist(userId);
            if (!isUserExist)
                return NotFound(isUserExist);
            return Ok(isUserExist);
            
        }
    }
}
