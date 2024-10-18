using HumanResourcesWebApi.Models.Requests.OrganizationStructures;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IOrganizationStructuresRepository
{
    Task<List<OrganizationStructureListDTO>> GetOrganizationStructureListAsync(bool includeCanceled = false);
    Task<OrganizationStructure> GetOrganizationStructureByIdAsync(int id);
    Task<UpdateOrganizationStructureRequest> UpdateOrganizationStrcuture(UpdateOrganizationStructureRequest request);
    Task<AddOrganizationStructureRequest> AddOrganizationStructureAsync(AddOrganizationStructureRequest request);
    Task DeleteOrganizationStructureAsync(int id);
    
}
