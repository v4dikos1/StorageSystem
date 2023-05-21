namespace Application.Common.Abstractions;

/// <summary>
/// Password hashing service
/// </summary>
public interface IPasswordService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}