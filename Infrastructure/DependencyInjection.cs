using Application.Common.Abstractions;
using Infrastructure.Authentication;
using Infrastructure.Registration;
using Infrastructure.Verification;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// An extension method for implementing dependencies in the Infrastructure layer
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<IEmailService, ConfirmationEmailService>();
        services.AddTransient<ITokenService, TokenService>();

        services.AddSingleton<IVerificationService, VerificationService>();

        return services;
    }
}