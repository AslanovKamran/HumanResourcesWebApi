using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IOrganizationStructuresRepository
{
    public Task<List<OrganizationStructureListDTO>> GetOrganizationStructureListAsync(bool includeCanceled = false);
    public Task<OrganizationStructure> GetOrganizationStructureByIdAsync(int id);
}
