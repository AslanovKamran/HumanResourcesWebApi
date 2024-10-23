using HumanResourcesWebApi.Models.Requests.FamilyMembers;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

namespace HumanResourcesWebApi.Abstract;

public interface IFamilyMembersRepository
{
    Task<List<EmployeeFamilyMember>> GetFamilyMembersAsync(int employeeId);
    Task AddFamilyMemberAsync(AddFamilyMemberRequest request);
    Task UpdateFamilyMemberAsync(UpdateFamilyMemberRequest request);
    Task DeleteFamilyMemberAsync(int id);
}
