using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Tokens;

public interface ITokenGenerator
{
    string GenerateAccessToken(User user);
}
