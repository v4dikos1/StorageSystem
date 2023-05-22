using Application.Common.Mapping;
using Application.Users.Commands.Registration;
using AutoMapper;

namespace StorageSystemApi.Models.UserModels;

/// <summary>
/// A dto model for user registration
/// </summary>
public record UserRegistrationDto : IMapWith<RegistrationCommand>
{
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegistrationDto, RegistrationCommand>();
    }
}