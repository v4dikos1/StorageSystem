namespace Infrastructure.Authentication;

/// <summary>
/// Model for storing jwt options.
/// Properties are initialized in the appsetting.json file or elsewhere.
/// </summary>
public class JwtOptions
{
    public string Issuer { get; init; }

    public string Audience { get; init; }

    public string SecretKey { get; init; }
}