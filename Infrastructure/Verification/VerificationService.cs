using System.Security.Cryptography;
using Application.Common.Abstractions;

namespace Infrastructure.Verification;

public class VerificationService : IVerificationService
{
    /// <summary>
    /// Confirmation code generation
    /// </summary>
    /// <returns></returns>
    public string CreateVerificationToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}