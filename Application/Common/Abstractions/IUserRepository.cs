using Application.Models;

namespace Application.Common.Abstractions;

public interface IUserRepository
{
    public User CreateUser(string username, string email, byte[] passwordHash, byte[] passwordSalt, bool isEmailConfirmed);

    public User UpdateUsername(Guid userId, string username);
    public User UpdateEmail(Guid userId, string email);
    public User UpdatePassword(Guid userId, byte[] passwordHash, byte[] passwordSalt);
    public User UpdateUser(Guid userId, string username, string email, byte[] passwordHash, byte[] passwordSalt, bool isEmailConfirmed);
    public User ConfirmEmail (Guid userId);

    public bool DeleteUser (Guid userId);

    public Task<List<User>> GetUsersAsync(int offset, int limit, CancellationToken cancellationToken);
    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}