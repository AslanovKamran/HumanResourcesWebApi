using HumanResourcesWebApi.Models.Requests.Roles;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IRolesRepository
{
    Task AddRoleAsync(AddRoleRequest request);
    Task<List<Role>> GetAllRolesAsync();
    Task UpdateRoleAsync(UpdateRoleRequest request);
}
