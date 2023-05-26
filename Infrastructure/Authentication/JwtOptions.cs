namespace Infrastructure.Authentication;

/// <summary>
/// Model for storing jwt options.
/// Properties are initialized in the appsetting.json file or elsewhere.
/// </summary>
public record JwtOptions
{
    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public required string SecretKey { get; init; }
}