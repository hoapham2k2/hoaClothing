using identity_service.Models;
using Microsoft.AspNetCore.Identity;

namespace identity_service.Repositories.RoleRepository;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }


    public async Task<ServiceResponse<string>> CreateRole(string roleName)
    {
        ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
        if (await CheckIfRoleExists(roleName))
        {
            serviceResponse.Data = roleName;
            serviceResponse.Success = false;
            serviceResponse.Messages = new List<string> {"Role already exists"};
            return serviceResponse;
        }

        IdentityRole role = new IdentityRole();
        role.Name = roleName;
        IdentityResult result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            serviceResponse.Data = roleName;
            serviceResponse.Success = false;
            serviceResponse.Messages = result.Errors.Select(e => e.Description);
            return serviceResponse;
        }

        serviceResponse.Data = role.Name;
        return serviceResponse;
    }
    
    //------> Helper methods <------//
    private async Task<bool> CheckIfRoleExists(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }
    
    
}