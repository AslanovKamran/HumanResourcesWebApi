using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Educations;

namespace HumanResourcesWebApi.Abstract;

public interface IEducationRepository
{
    public Task<List<EmployeeEducation>> GetEmployeeEducationAsync(int employeeId);
    public Task AddEmployeeEducationAsync(AddEmployeeEducationRequest education);
    public Task UpdateEmployeeEducationAsync(UpdateEmployeeEducationRequest education);
    public Task DeleteEmployeeEducationAsync(int id);

}
