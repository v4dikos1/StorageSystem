using Application.Common.Mapping;
using AutoMapper;

namespace Application.Models;

public class User : IMapWith<Domain.Entities.User>
{
    public Guid Id { get; set; }

    public required string Username { get; set; }
    public required string Email { get; set; }

    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

    public bool IsEmailConfirmed { get; set; } = false;
    public string? VerificationToken { get; set; }

    public required DateTime RegisteredAt { get; set; }

    public List<File> UploadedFiles { get; set; } = new List<File>();

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, Domain.Entities.User>();
    }
}