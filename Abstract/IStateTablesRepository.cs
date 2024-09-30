using HumanResourcesWebApi.Models.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Abstract;

public interface IStateTablesRepository
{
    Task<(List<StateTable> stateTables, PageInfo pageInfo)> GetStateTablesAsync(int itemsPerPage = 30, int currentPage = 1, bool showOnlyActive = true);
    Task<StateTable> GetByOrganizationAsync(int organizationId);

}
