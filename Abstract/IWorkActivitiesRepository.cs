using HumanResourcesWebApi.Models.Requests.WorkActivities;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IWorkActivitiesRepository
{
    Task<List<EmployeeWorkActivity>> GetEmployeeWorkActivityAsync(int employeeId);
    Task UpdateWorkActivityAsync(UpdateWorkActivityRequest request);
    Task DeleteWorkActivityAsync(int id);
    Task AddWorkActivityAsync(AddWorkActivityRequest request);  
} 
