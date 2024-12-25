using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain.TabelModels;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class TabelRepository : ITabelRepository
{
    private readonly string _connectionString;
    public TabelRepository(string connectionString) => _connectionString = connectionString;
     

    public async Task<IEnumerable<TabelModel>> GetTabelDataAsync(int year, int month, int? organizationStructureId)
    {

        var parameters = new DynamicParameters();
        parameters.Add("Year", year, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Month", month, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrganizationStructureId", organizationStructureId, DbType.Int32, ParameterDirection.Input);

        var query = @"GetEmployeesWithFullDetails";

        using (var db = new SqlConnection(_connectionString))
        {

            //var result = await db.QueryAsync<TabelModel>(query, parameters, commandType: CommandType.StoredProcedure);
            //return result.ToList();
            var result = await db.QueryAsync(query, parameters, commandType: CommandType.StoredProcedure);

            var tabel = result.Select(row => new TabelModel
            {
                Id = row.Id,
                TabelNumber = row.TabelNumber,
                FinCode = row.FinCode,
                SocialInsuranceNumber = row.SocialInsuranceNumber,
                Name = row.Name,
                StateTableName = row.StateTableName,
                OrganizationName = row.OrganizationName,
                Degree = row.Degree,
                WorkType = row.WorkType,
                WorkHours = row.WorkHours,
                WorkHoursSaturday = row.WorkHoursSaturday,

                BusinessTrips = JsonConvert.DeserializeObject<List<TabelModelBusinessTrips>>(row.BusinessTrips ?? "[]"),
                ExtraWork = JsonConvert.DeserializeObject<List<TabelModelExtraWork>>(row.ExtraWork ?? "[]"),
                Invalidity = JsonConvert.DeserializeObject<List<TabelModelBulletin>>(row.Invalidity ?? "[]"),
                Absences = JsonConvert.DeserializeObject<List<TabelModelAbsence>>(row.Absences ?? "[]"),
                Vacations = JsonConvert.DeserializeObject<List<TabelModelVacation>>(row.Vacations ?? "[]")

                // Map JSON to Categories


            }).ToList();

            return tabel;
        }
    }
}
