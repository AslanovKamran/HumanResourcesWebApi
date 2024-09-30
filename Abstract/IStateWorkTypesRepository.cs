using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IStateWorkTypesRepository
{
    Task<List<StateWorkType>> GetStateWorkTypesAsync();
    Task<StateWorkType> GetStateWorkTypeByIdAsync(int id);
}
