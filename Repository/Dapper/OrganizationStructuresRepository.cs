using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using Dapper;
using HumanResourcesWebApi.Models.DTO;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class OrganizationStructuresRepository : IOrganizationStructuresRepository
{
    private readonly string _connectionString;

    public OrganizationStructuresRepository(string connectionString) => _connectionString = connectionString;

    public async Task<OrganizationStructure> GetOrganizationStructureByIdAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            string query = @"exec GetOrganizationStructureById @Id";
            var orgstruct = await db.QueryFirstOrDefaultAsync<OrganizationStructure>(query, parameters);
            return orgstruct!;
        }
    }

    public async Task<List<OrganizationStructureListDTO>> GetOrganizationStructureListAsync(bool includeCanceled)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query;

            // Fetch all structures from the database
            if (includeCanceled)
                query = @"exec OrganizationStructures_GetOrganizationStructures";

            //Fetch only existing structures
            else query = @"exec OrganizationStructures_GetExistingOrganizationStructures";

            var structures = (await connection.QueryAsync<OrganizationStructureListDTO>(query)).ToList();

            var lookup = structures.ToLookup(s => s.ParentId);

            var hierarchy = BuildHierarchy(lookup, null);

            return hierarchy;
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
