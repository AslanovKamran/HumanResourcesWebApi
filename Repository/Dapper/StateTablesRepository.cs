using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.Requests;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class StateTablesRepository : IStateTablesRepository
    {
        private readonly string _connectionString;

        public StateTablesRepository(string connectionString) => _connectionString = connectionString;
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
        public async Task<List<StateTable>> GetByOrganizationAsync(int organizationId, bool showOnlyActive = true)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("OrganizationId", organizationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("ShowOnlyActive", showOnlyActive, DbType.Boolean, ParameterDirection.Input);

                string query = @"GetStateTablesByOrganizationId @OrganizationId, @ShowOnlyActive";

                // Use Dapper to map the results from the procedure
                var result = (await db.QueryAsync<StateTable, OrganizationStructure, StateWorkType, StateTable>(
                    query,
                    (stateTable, org, type) =>
                    {
                        stateTable.OrganizationStructure = org;
                        stateTable.StateWorkType = type;
                        return stateTable;
                    },
                    parameters
                )).ToList();

                return result;
            }
        }

        public async Task AddStateTableAsync(AddStateTableRequest request)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", request.Name, DbType.String);
                parameters.Add("UnitCount", request.UnitCount, DbType.Int32);
                parameters.Add("MonthlySalaryFrom", request.MonthlySalaryFrom, DbType.Int32);
                parameters.Add("MonthlySalaryTo", request.MonthlySalaryTo, DbType.Int32);
                parameters.Add("OccupiedPostCount", request.OccupiedPostCount, DbType.Int32);
                parameters.Add("DocumentNumber", request.DocumentNumber, DbType.String);
                parameters.Add("DocumentDate", request.DocumentDate, DbType.DateTime);
                parameters.Add("OrganizationStructureId", request.OrganizationStructureId, DbType.Int32);
                parameters.Add("WorkTypeId", request.WorkTypeId, DbType.Int32);
                parameters.Add("WorkHours", request.WorkHours, DbType.Int32);
                parameters.Add("WorkHoursSaturday", request.WorkHoursSaturday, DbType.Int32);
                parameters.Add("TabelPosition", request.TabelPosition, DbType.Int32);
                parameters.Add("TabelPriority", request.TabelPriority, DbType.Int32);
                parameters.Add("ExcludeBankomat", request.ExcludeBankomat, DbType.Int32);
                parameters.Add("Degree", request.Degree, DbType.Int32);
                parameters.Add("HourlySalary", request.HourlySalary, DbType.Decimal);
                parameters.Add("IsCanceled", request.IsCanceled, DbType.Boolean);
                parameters.Add("WorkingHoursSpecial", request.WorkingHoursSpecial, DbType.String);
                parameters.Add("MonthlySalaryExtra", request.MonthlySalaryExtra, DbType.Int32);
                parameters.Add("HarmfulnessCoefficient", request.HarmfulnessCoefficient, DbType.Int32);

                string query = @"exec AddStateTable 
                                                 @Name,
                                                 @UnitCount,
                                                 @MonthlySalaryFrom,
                                                 @MonthlySalaryTo,
                                                 @OccupiedPostCount,
                                                 @DocumentNumber,
                                                 @DocumentDate,
                                                 @OrganizationStructureId,
                                                 @WorkTypeId,
                                                 @WorkHours,
                                                 @WorkHoursSaturday,
                                                 @TabelPosition,
                                                 @TabelPriority,
                                                 @ExcludeBankomat,
                                                 @Degree,
                                                 @HourlySalary,
                                                 @IsCanceled,
                                                 @WorkingHoursSpecial,
                                                 @MonthlySalaryExtra,
                                                 @HarmfulnessCoefficient";

                await db.ExecuteAsync(query, parameters); // Execute without expecting a result
            }

        }

        public async Task UpdateStateTableAsync(UpdateStateTableRequest request)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("Degree", request.Degree, DbType.Int32, ParameterDirection.Input);
                parameters.Add("UnitCount", request.UnitCount, DbType.Int32, ParameterDirection.Input);
                parameters.Add("MonthlySalaryFrom", request.MonthlySalaryFrom, DbType.Int32, ParameterDirection.Input);
                parameters.Add("HourlySalary", request.HourlySalary, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("MonthlySalaryExtra", request.MonthlySalaryExtra, DbType.Int32, ParameterDirection.Input);
                parameters.Add("OccupiedPostCount", request.OccupiedPostCount, DbType.Int32, ParameterDirection.Input);
                parameters.Add("DocumentNumber", request.DocumentNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("DocumentDate", request.DocumentDate, DbType.Date, ParameterDirection.Input);
                parameters.Add("WorkTypeId", request.WorkTypeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("OrganizationStructureId", request.OrganizationStructureId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("HarmfulnessCoefficient", request.HarmfulnessCoefficient, DbType.Int32, ParameterDirection.Input);
                parameters.Add("WorkHours", request.WorkHours, DbType.Int32, ParameterDirection.Input);
                parameters.Add("WorkingHoursSpecial", request.WorkingHoursSpecial, DbType.String, ParameterDirection.Input);
                parameters.Add("WorkHoursSaturday", request.WorkHoursSaturday, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TabelPriority", request.TabelPriority, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TabelPosition", request.TabelPosition, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IsCanceled", request.IsCanceled, DbType.Boolean, ParameterDirection.Input);

                string query = @"exec UpdateStateTable
                                                    @Id, @Name, @Degree, @UnitCount, @MonthlySalaryFrom, 
                                                    @HourlySalary, @MonthlySalaryExtra, @OccupiedPostCount, @DocumentNumber,
                                                    @DocumentDate, @WorkTypeId, @OrganizationStructureId, @HarmfulnessCoefficient, 
                                                    @WorkHours, @WorkingHoursSpecial, @WorkHoursSaturday, @TabelPriority, 
                                                    @TabelPosition, @IsCanceled";

                await db.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteStateTableAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString)) 
            {

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
                var query = @"exec SoftDeleteStateTable @Id";
                await db.ExecuteAsync(query, parameters); // Execute without expecting a result
            }
        }
    }
}
