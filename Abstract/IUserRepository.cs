using HumanResourcesWebApi.Models.Requests.Users;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<int> SignUpUserAsync(SignUpUserRequest request);
    Task<User> GetUserByIdUserNameAsync(string userName);
    Task ChangeUserPasswordByIdAsync(ChangePasswordRequest request);
}
