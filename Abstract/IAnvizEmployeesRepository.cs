using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IAnvizEmployeesRepository
{
    Task<List<AnvizEmployee>> GetAnvizEmployeesAsync(int? organizationStructureId);
}
