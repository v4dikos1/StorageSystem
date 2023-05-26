using Application.Models;

namespace Application.Common.Abstractions;

public interface IUserRepository
{
    public User CreateUser(string username, string email, byte[] passwordHash, byte[] passwordSalt, bool isEmailConfirmed);

    public Task<User> ConfirmEmailAsync(string verificationToken, CancellationToken cancellationToken);

    public Task<bool> DeleteUser (Guid userId, CancellationToken cancellationToken);

    public Task<List<User>> GetUsersAsync(int offset, int limit, CancellationToken cancellationToken);
    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}