using identity_service.Models;

namespace identity_service.Repositories.RoleRepository;

public interface IRoleRepository
{
    //Create a role
    Task<ServiceResponse<string>> CreateRole(string roleName);
}