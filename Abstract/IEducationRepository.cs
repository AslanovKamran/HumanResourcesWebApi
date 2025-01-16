using HumanResourcesWebApi.Models.Requests.Educations;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IEducationRepository
{
    public Task<(PageInfo PageInfo, List<EmployeeEducation> Educations)> GetAllEmployeeEducationAsync(int itemsPerPage = 10, int currentPage = 1);
    public Task<List<EmployeeEducation>> GetEmployeeEducationAsync(int employeeId);
    public Task AddEmployeeEducationAsync(AddEmployeeEducationRequest education);
    public Task UpdateEmployeeEducationAsync(UpdateEmployeeEducationRequest education);
    public Task DeleteEmployeeEducationAsync(int id);

}
