using Application.Common.Mapping;
using Application.Models;
using AutoMapper;

namespace Application.Users.Queries.GetUserProfile;

/// <summary>
/// Model for user representation
/// </summary>
public class UserProfileVm : IMapWith<User>
{
    public Guid Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserProfileVm>();
    }
}