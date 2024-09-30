using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class StateTablesRepository : IStateTablesRepository
    {
        private readonly string _connectionString;

        public StateTablesRepository(string connectionString) => _connectionString = connectionString;

        public Task<StateTable> GetByOrganizationAsync(int organizationId)
        {
            throw new NotImplementedException(); !!!!
        }


        public async Task<(List<StateTable> stateTables, PageInfo pageInfo)> GetStateTablesAsync(int itemsPerPage, int currentPage, bool showOnlyActive)
        {
            int skip = (currentPage - 1) * itemsPerPage;
            int take = itemsPerPage;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("ShowOnlyActive", showOnlyActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("Skip", skip, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Take", take, DbType.Int32, ParameterDirection.Input);

                string query = @"GetStateTablesWithCount @ShowOnlyActive, @Skip, @Take";

                // Execute the query and return multiple result sets (total count and paginated data)
                using (var multi = await db.QueryMultipleAsync(query, parameters))
                {
                    // Retrieve the total count
                    int totalCount = multi.ReadSingle<int>();

                    // Retrieve the paginated state tables
                    var stateTables = multi.Read<StateTable, OrganizationStructure, StateWorkType, StateTable>(
                        (stateTable, org, type) =>
                        {
                            stateTable.OrganizationStructure = org;
                            stateTable.StateWorkType = type;
                            return stateTable;
                        }
                    ).ToList();

                    // Create the PageInfo object
                    var pageInfo = new PageInfo(totalCount, itemsPerPage, currentPage);

                    return (stateTables, pageInfo);
                }
            }
        }


    }
}
