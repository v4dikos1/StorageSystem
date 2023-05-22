using Application.Users.Commands.Login;
using Application.Users.Commands.Registration;
using Application.Users.Queries.GetUserProfile;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ValidationBehaviorDependencyInjection
{
    /// <summary>
    /// An extension method for implementing validation dependencies in the Application layer
    /// </summary>
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegistrationCommand>, RegistrationCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<GetUserProfileQuery>, GetUserProfileQueryValidator>();

        return services;
    }
}