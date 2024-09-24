using HumanResourcesWebApi.Models.Requests;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IOrganizationStructuresRepository
{
    public Task<List<OrganizationStructureListDTO>> GetOrganizationStructureListAsync(bool includeCanceled = false);
    public Task<OrganizationStructure> GetOrganizationStructureByIdAsync(int id);
    public Task<UpdateOrganizationStructureRequest> UpdateOrganizationStrcuture(UpdateOrganizationStructureRequest request);
    public Task<AddOrganizationStructureRequest> AddOrganizationStructureAsync(AddOrganizationStructureRequest request);
}
