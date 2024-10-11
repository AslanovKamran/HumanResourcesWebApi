using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.WorkActivities;

namespace HumanResourcesWebApi.Abstract;

public interface IWorkActivitiesRepository
{
    Task<List<EmployeeWorkActivity>> GetEmployeeWorkActivityAsync(int employeeId);
    Task UpdateWorkActivityAsync(UpdateWorkActivityRequest request);
    Task DeleteWorkActivityAsync(int id);
    Task AddWorkActivityAsync(AddWorkActivityRequest request);  
} 
