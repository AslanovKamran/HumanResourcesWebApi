using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.Requests.OrganizationStructures;

namespace HumanResourcesWebApi.Repository.Dapper;

public class OrganizationStructuresRepository : IOrganizationStructuresRepository
{
    private readonly string _connectionString;

    public OrganizationStructuresRepository(string connectionString) => _connectionString = connectionString;

    public async Task<AddOrganizationStructureRequest> AddOrganizationStructureAsync(AddOrganizationStructureRequest request)
    {

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Code", request.Code, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("BeginningHistory", request.BeginningHistory, DbType.Date, ParameterDirection.Input);
            parameters.Add("FirstNumber", request.FirstNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("SecondNumber", request.SecondNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("ParentId", request.ParentId, DbType.Int32, ParameterDirection.Input);

            // Make sure the procedure name matches your actual stored procedure
            string query = @"AddOrganizationStructure";
                         

            // Use Dapper to execute the stored procedure and return the inserted entity
            var insertedOrganization = await db.QuerySingleOrDefaultAsync<AddOrganizationStructureRequest>(query, parameters,commandType:CommandType.StoredProcedure);

            return insertedOrganization ?? throw new InvalidOperationException("Failed to insert organization structure.");
        }
    }

   
    public async Task<OrganizationStructure> GetOrganizationStructureByIdAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            string query = @"Select * FROM OrganizationStructures WHERE Id = @Id";
            var orgstruct = await db.QueryFirstOrDefaultAsync<OrganizationStructure>(query, parameters);
            return orgstruct!;
        }
    }

    public async Task<List<OrganizationStructureListDTO>> GetOrganizationStructureListAsync(bool includeCanceled = false)
    {
        var parameters = new DynamicParameters();
        parameters.Add("IncludeCanceled", includeCanceled, DbType.Boolean, ParameterDirection.Input);
        
        string query = "GetOrganizationStructures";
        using (var connection = new SqlConnection(_connectionString))
        {
            var structures = (await connection.QueryAsync<OrganizationStructureListDTO>(query, parameters, commandType: CommandType.StoredProcedure)).AsList();

            var lookup = structures.ToLookup(s => s.ParentId);

            var hierarchy = BuildHierarchy(lookup, null);

            return hierarchy;
        }
    }
    public async Task<UpdateOrganizationStructureRequest> UpdateOrganizationStrcuture(UpdateOrganizationStructureRequest request)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Code", request.Code, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("BeginningHistory", request.BeginningHistory, DbType.Date, ParameterDirection.Input);
            parameters.Add("FirstNumber", request.FirstNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("SecondNumber", request.SecondNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("TabelOrganizationId", request.TabelOrganizationId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("TabelPriority", request.TabelPriority, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Canceled", request.Canceled, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("HeadName", request.HeadName, DbType.String, ParameterDirection.Input);
            parameters.Add("HeadPosition", request.HeadPosition, DbType.String, ParameterDirection.Input);
            parameters.Add("IsSeaCoef", request.IsSeaCoef, DbType.Boolean, ParameterDirection.Input);

            // Make sure the procedure name matches your actual stored procedure
            string query = @"UpdateOrganizationStructure";
                                
            // Use Dapper to execute the stored procedure and return the inserted entity
            var updatedOrganization = await db.QuerySingleOrDefaultAsync<UpdateOrganizationStructureRequest>(query, parameters, commandType: CommandType.StoredProcedure);

            return updatedOrganization ?? throw new InvalidOperationException("Failed to update organization structure.");
        }
    }

    public async Task DeleteOrganizationStructureAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            var query = "SoftDeleteOrganizationStructure";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure); 
        }
    }


    // Helper method to build hierarchy for DTOs
    private List<OrganizationStructureListDTO> BuildHierarchy(ILookup<int?, OrganizationStructureListDTO> lookup, int? parentId)
    {
        var result = new List<OrganizationStructureListDTO>();

        foreach (var item in lookup[parentId])
        {
            // Recursively get the children of the current item
            item.Children = BuildHierarchy(lookup, item.Id); // The type here should be DTO

            // Add the item with its children to the result
            result.Add(item);  // Make sure the result is a List<OrganizationStructureListDTO>
        }

        return result;  // Return a List of DTOs
    }


}
