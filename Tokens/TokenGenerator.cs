using HumanResourcesWebApi.Models.Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace HumanResourcesWebApi.Tokens;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _options;
    public TokenGenerator(IOptions<JwtOptions> options) => _options = options.Value;

    public string GenerateAccessToken(User user)
    {
        var issuedAt = DateTime.Now;

        // Generate basic claims
        var claims = new List<Claim>
    {
        new ("id", user.Id.ToString()),
        new ("userName", user.UserName),
        new ("fullName", user.FullName!),
        new ("role", user.Role!),
        new ("iat", ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64),

        
    };

        // Serialize rights as a JSON array
        var rightsJson = System.Text.Json.JsonSerializer.Serialize(user.Rights);

        // Create the JWT payload
        var payload = new JwtPayload(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: issuedAt,
            expires: issuedAt + _options.AccessValidFor
        );

        // Add rights as a parsed JSON object
        payload["rights"] = System.Text.Json.JsonSerializer.Deserialize<object>(rightsJson);

        // Create the token
        var token = new JwtSecurityToken(
            new JwtHeader(_options.SigningCredentials),
            payload
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private static long ToUnixEpochDate(DateTime date)
    {
        var offset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        return (long)Math.Round((date.ToUniversalTime() - offset).TotalSeconds);
    }

}
