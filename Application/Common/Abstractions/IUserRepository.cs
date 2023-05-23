using Application.Models;

namespace Application.Common.Abstractions;

public interface IUserRepository
{
    public User CreateUser(string username, string email, byte[] passwordHash, byte[] passwordSalt, bool isEmailConfirmed);

    public Task<User> UpdateUsernameAsync(Guid userId, string username, CancellationToken cancellationToken);
    public Task<User> UpdateEmailAsync(Guid userId, string email, CancellationToken cancellationToken);
    public Task<User> UpdatePasswordAsync(Guid userId, byte[] passwordHash, byte[] passwordSalt, CancellationToken cancellationToken);
    public Task<User> UpdateUserAsync(Guid userId, string username, string email, byte[] passwordHash,
        byte[] passwordSalt, bool isEmailConfirmed, CancellationToken cancellationToken);
    public Task<User> ConfirmEmailAsync(string verificationToken, CancellationToken cancellationToken);

    public Task<bool> DeleteUser (Guid userId, CancellationToken cancellationToken);

    public Task<List<User>> GetUsersAsync(int offset, int limit, CancellationToken cancellationToken);
    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}