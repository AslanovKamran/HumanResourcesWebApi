using Azure.Core;
using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Common.AuthenticationService;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository(string connectionString) => _connectionString = connectionString;

    #region Get


    public async Task<List<User>> GetAllUsersAsync()
    {
        const string query = "GetAllUsersInfo";

        using (var db = new SqlConnection(_connectionString))
        {


            var result = await db.QueryAsync<dynamic>(query, commandType: CommandType.StoredProcedure);

            var users = result.Select(row => new User
            {
                Id = row.Id,
                UserName = row.UserName,
                Password = row.Password,
                Salt = row.Salt,
                FullName = row.FullName,
                RoleId = row.RoleId,
                Role = row.Role,
                Structure = row.Structure,
                InsertedBy = row.InsertedBy,
                InsertedAt = row.InsertedAt,
                UpdatedBy = row.UpdatedBy,
                UpdatedAt = row.UpdatedAt,
                PasswordUpdatedBy = row.PasswordUpdatedBy,
                PasswordUpdatedAt = row.PasswordUpdatedAt,
                CanEdit = row.CanEdit,
                Rights = string.IsNullOrWhiteSpace(row.Rights)
                    ? new List<Right>()
                    : JsonConvert.DeserializeObject<List<Right>>(row.Rights) ?? new List<Right>()
            }).ToList();

            return users;
        }
    }


    public async Task<User> GetUserByIdAsync(int id)
    {
        var query = @"GetUser";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {

            var result = await db.QuerySingleOrDefaultAsync(query, parameters, commandType: CommandType.StoredProcedure);


            if (result == null) return null!;

            var user = new User
            {
                Id = result.Id,
                UserName = result.UserName,
                Password = result.Password,
                Salt = result.Salt,
                FullName = result.FullName,
                RoleId = result.RoleId,
                Role = result.Role,
                Structure = result.Structure,
                InsertedBy = result.InsertedBy,
                InsertedAt = result.InsertedAt,
                UpdatedBy = result.UpdatedBy,
                UpdatedAt = result.UpdatedAt,
                PasswordUpdatedBy = result.PasswordUpdatedBy,
                PasswordUpdatedAt = result.PasswordUpdatedAt,
                CanEdit = result.CanEdit,
                Rights = string.IsNullOrWhiteSpace(result.Rights)
            ? new List<Right>()
            : JsonConvert.DeserializeObject<List<Right>>(result.Rights) ?? new List<Right>()
            };

            return user;

        }
    }


    public async Task<User> GetUserByIdUserNameAsync(string userName)
    {
        var query = @"GetUserByUserName";

        var parameters = new DynamicParameters();
        parameters.Add("UserName", userName, DbType.String, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {

            var result = await db.QuerySingleOrDefaultAsync(query, parameters, commandType: CommandType.StoredProcedure);


            if (result == null) return null!;

            var user = new User
            {
                Id = result.Id,
                UserName = result.UserName,
                Password = result.Password,
                Salt = result.Salt,
                FullName = result.FullName,
                RoleId = result.RoleId,
                Role = result.Role,
                Structure = result.Structure,
                InsertedBy = result.InsertedBy,
                InsertedAt = result.InsertedAt,
                UpdatedBy = result.UpdatedBy,
                UpdatedAt = result.UpdatedAt,
                PasswordUpdatedBy = result.PasswordUpdatedBy,
                PasswordUpdatedAt = result.PasswordUpdatedAt,
                CanEdit = result.CanEdit,
                Rights = string.IsNullOrWhiteSpace(result.Rights)
            ? new List<Right>()
            : JsonConvert.DeserializeObject<List<Right>>(result.Rights) ?? new List<Right>()
            };

            return user;
        }
    }

    #endregion

    #region Add

    public async Task<int> SignUpUserAsync(SignUpUserRequest request)
    {
        // Generate the salt and hash the password
        request.Salt = AuthService.GenerateSalt();
        request.Password = AuthService.HashPassword(request.Password, request.Salt);

        var parameters = new DynamicParameters();

        // Add input parameters
        parameters.Add("UserName", request.UserName, DbType.String, ParameterDirection.Input);
        parameters.Add("Password", request.Password, DbType.String, ParameterDirection.Input);
        parameters.Add("Salt", request.Salt, DbType.String, ParameterDirection.Input);
        parameters.Add("FullName", request.FullName, DbType.String, ParameterDirection.Input);
        parameters.Add("RoleId", request.RoleId, DbType.Int32, ParameterDirection.Input);

        // Handle nullable StructureId and InsertedBy
        parameters.Add("StructureId", request.StructureId ?? (object)DBNull.Value, DbType.Int32, ParameterDirection.Input);
        parameters.Add("InsertedBy", string.IsNullOrWhiteSpace(request.InsertedBy) ? (object)DBNull.Value : request.InsertedBy, DbType.String, ParameterDirection.Input);

        parameters.Add("CanEdit", request.CanEdit, DbType.Boolean, ParameterDirection.Input);

        // Handle nullable Rights
        parameters.Add("Rights", string.IsNullOrWhiteSpace(request.Rights) ? (object)DBNull.Value : request.Rights, DbType.String, ParameterDirection.Input);

        // Add output parameter for the new user ID
        parameters.Add("NewUserId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var query = "InsertUser";

        using (var db = new SqlConnection(_connectionString))
        {
            // Execute the stored procedure
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

            // Retrieve the new user ID from the output parameter
            int newUserId = parameters.Get<int>("NewUserId");

            return newUserId;
        }
    }



    #endregion

    #region Change Password

    public async Task ChangeUserPasswordByIdAsync(ChangePasswordRequest request)
    {
        var user = await GetUserByIdAsync(request.Id);
        var userSalt = user.Salt;

        var newPassword = AuthService.HashPassword(request.NewPassword, user.Salt);

        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("NewPassword", newPassword, DbType.String, ParameterDirection.Input);

        var query = @"ChangePassword";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }


    }

    #endregion
}
