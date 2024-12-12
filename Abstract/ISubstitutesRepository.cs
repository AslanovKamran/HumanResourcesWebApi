using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Substitutes;

namespace HumanResourcesWebApi.Abstract;

public interface ISubstitutesRepository
{
    Task<List<SubstituteByVacationDTO>> GetSubstituteByVacationsAsync(int whomId, int vacationId);
    Task<List<SubstituteByBulletinDTO>> GetSubstituteByBulletinAsync(int whomId, int tabelVacationId);
    Task AddSubstituteAsync(AddSubstituteRequest request);
    Task UpdateSubstituteAsync(UpdateSubstituteRequest request);
    Task DeleteSubstituteByIdAsync(int id);
}
