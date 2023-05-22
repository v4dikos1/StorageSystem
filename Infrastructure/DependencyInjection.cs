using Application.Common.Abstractions;
using Infrastructure.Authentication;
using Infrastructure.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}