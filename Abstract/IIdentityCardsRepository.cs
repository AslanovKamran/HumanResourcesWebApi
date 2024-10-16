using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.IdentityCards;

namespace HumanResourcesWebApi.Abstract;

public interface IIdentityCardsRepository
{
    Task<IdentityCard> GetIdentityCardAsync(int employeeId);
    Task AddIdentityCardAsync(AddIdentityCardRequest request);

    Task UpdateIdentityCardAsync(UpdateIdentityCardRequest request);
}
