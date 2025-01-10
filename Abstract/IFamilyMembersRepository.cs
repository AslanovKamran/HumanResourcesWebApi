using HumanResourcesWebApi.Models.Requests.FamilyMembers;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IFamilyMembersRepository
{
    Task<List<EmployeeFamilyMember>> GetFamilyMembersAsync(int employeeId);
    Task<(PageInfo PageInfo, List<EmployeeFamilyMember> FamilyMembers)> GetAllFamilyMembers(int itemsPerPage = 10, int currentPage = 1);

    Task AddFamilyMemberAsync(AddFamilyMemberRequest request);
    Task UpdateFamilyMemberAsync(UpdateFamilyMemberRequest request);
    Task DeleteFamilyMemberAsync(int id);
}
