using HumanResourcesWebApi.Models.Requests.IdentityCards;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IIdentityCardsRepository
{
    Task<IdentityCard> GetIdentityCardAsync(int employeeId);
    Task AddIdentityCardAsync(AddIdentityCardRequest request);
    Task UpdateIdentityCardAsync(UpdateIdentityCardRequest request);
}
