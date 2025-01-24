using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Brigades;

namespace HumanResourcesWebApi.Abstract;

public interface IBrigadesRepository
{
    Task<List<Brigade>> GetBrigadesAsync();
    Task<Brigade> GetBrigadeByIdAsync(int id);
    Task AddBrigadeAsync(AddBrigadeRequest request);
    Task UpdateBrigadeAsync(UpdateBrigadeRequest request);
    Task DeleteBrigadeAsync(int id);
    Task AssignNewBrigadeToEmployeeAsync(int employeeId, int brigadeId);
    Task UnassignFromAllBrigades(int employeeId);
    Task<List<BrigadeDto>> GetEmployeesByBrigadeId(int id);
}
