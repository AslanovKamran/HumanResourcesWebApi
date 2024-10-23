using HumanResourcesWebApi.Models.Requests.StateTables;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class StateTablesRepository(string connectionString) : IStateTablesRepository
{
    private readonly string _connectionString = connectionString;

    #region Get

    public async Task<(List<StateTable> stateTables, PageInfo pageInfo)> GetStateTablesAsync(int itemsPerPage, int currentPage, bool showOnlyActive = true)
    {
        int skip = (currentPage - 1) * itemsPerPage;
        int take = itemsPerPage;

        var parameters = new DynamicParameters();
        parameters.Add("ShowOnlyActive", showOnlyActive, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("Skip", skip, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Take", take, DbType.Int32, ParameterDirection.Input);

        var query = @"GetStateTablesWithCount @ShowOnlyActive, @Skip, @Take";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {

            using (var multi = await db.QueryMultipleAsync(query, parameters))
            {
                int totalCount = multi.ReadSingle<int>();

                var stateTables = multi.Read<StateTable, OrganizationStructure, StateWorkType, StateTable>(
                    (stateTable, org, type) =>
                    {
                        stateTable.OrganizationStructure = org;
                        stateTable.StateWorkType = type;
                        return stateTable;
                    }
                ).AsList();

                var pageInfo = new PageInfo(totalCount, itemsPerPage, currentPage);

                return (stateTables, pageInfo);
            }
        }
    }
    public async Task<List<StateTable>> GetByOrganizationAsync(int organizationId, bool showOnlyActive = true)
    {
        var parameters = new DynamicParameters();
        parameters.Add("OrganizationId", organizationId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("ShowOnlyActive", showOnlyActive, DbType.Boolean, ParameterDirection.Input);

        string query = @"GetStateTablesByOrganizationId @OrganizationId, @ShowOnlyActive";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var result = (await db.QueryAsync<StateTable, OrganizationStructure, StateWorkType, StateTable>(
                query,
                (stateTable, org, type) =>
                {
                    stateTable.OrganizationStructure = org;
                    stateTable.StateWorkType = type;
                    return stateTable;
                },
                parameters
            )).AsList();

            return result;
        }
    }

    #endregion

    #region Add
    public async Task AddStateTableAsync(AddStateTableRequest request)
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

        string query = @"AddStateTable";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Update
    public async Task UpdateStateTableAsync(UpdateStateTableRequest request)
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

        string query = @"UpdateStateTable";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeleteStateTableAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
        var query = @"SoftDeleteStateTable";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure); 
        }
    }

    #endregion

}
