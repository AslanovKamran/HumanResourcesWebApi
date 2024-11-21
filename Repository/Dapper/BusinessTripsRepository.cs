using HumanResourcesWebApi.Models.Requests.BusinessTrips;
using HumanResourcesWebApi.Models.DTO.BusinessTrip;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class BusinessTripsRepository(string connectionString) : IBusinessTripsRepository
{
    private readonly string _connectionString = connectionString;

    public async Task AddBusinessTripWithDetailsAsync(AddBusinessTripWithDetailsRequest request)
    {
        var uniqueEmployeeIds = string.Join(",", request.EmployeeIds.Split(',').Select(int.Parse).Distinct());

        using (var connection = new SqlConnection(_connectionString))
        {

            var parameters = new DynamicParameters();
            parameters.Add("Purpose", request.Purpose, DbType.String, ParameterDirection.Input);
            parameters.Add("StartDate", request.StartDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("EndDate", request.EndDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("EmployeeIds", uniqueEmployeeIds, DbType.String, ParameterDirection.Input);
            parameters.Add("CityIds", request.CityIds, DbType.String, ParameterDirection.Input);
            parameters.Add("DestinationPoints", request.DestinationPoints, DbType.String, ParameterDirection.Input);
            parameters.Add("DocumentNumber", request.DocumentNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("DocumentDate", request.DocumentDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("TripCardGivenAt", request.TripCardGivenAt, DbType.Date, ParameterDirection.Input);
            parameters.Add("TripCardNumber", request.TripCardNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("OrganizationInCharge", request.OrganizationInCharge, DbType.String, ParameterDirection.Input);
            parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);

            await connection.ExecuteAsync("AddBusinessTripWithDetails", parameters, commandType: CommandType.StoredProcedure);

        }
    }

    public async Task<BusinessTripDetailsDTO> GetBusinessTripDetailsAsync(int tripId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("TripId", tripId, DbType.Int32, ParameterDirection.Input);

            var query = "GetBusinessTripDetails";

            using (var multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure))
            {
                // Read BusinessTrip details
                var businessTrip = await multi.ReadSingleAsync<BusinessTripDTO>();

                // Read TripEmployees details
                var employees = (await multi.ReadAsync<TripEmployeeDTO>()).AsList();

                // Read TripCities details
                var cities = (await multi.ReadAsync<TripCityDTO>()).AsList();

                // Combine the results into a single DTO
                var result = new BusinessTripDetailsDTO
                {
                    BusinessTrip = businessTrip,
                    Employees = employees,
                    Cities = cities
                };

                return result;
            }
        }
    }
    public async Task<List<BusinessTripDTO>> GetBusinessTrips()
    {
        var query = "GetBusinessTrips";
        using (var dbConnection = new SqlConnection(_connectionString))
        {
            var result = await dbConnection.QueryAsync<BusinessTripDTO>(query);
            return result.AsList();
        }
    }
}
