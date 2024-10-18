using HumanResourcesWebApi.Models.Requests.StateTables;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IStateTablesRepository
{
    Task<(List<StateTable> stateTables, PageInfo pageInfo)> GetStateTablesAsync(int itemsPerPage = 30, int currentPage = 1, bool showOnlyActive = true);
    Task <List<StateTable>> GetByOrganizationAsync(int organizationId, bool showOnlyActive = true);
    Task AddStateTableAsync(AddStateTableRequest request);
    Task UpdateStateTableAsync(UpdateStateTableRequest request);
    Task DeleteStateTableAsync(int id);
}
