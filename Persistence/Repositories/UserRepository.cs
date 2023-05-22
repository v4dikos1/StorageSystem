using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using User = Application.Models.User;

namespace Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public User CreateUser(string username, string email, byte[] passwordHash, byte[] passwordSalt, bool isEmailConfirmed)
    {
        var user = _context.Users.Add(new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsEmailConfirmed = isEmailConfirmed,
            Id = Guid.NewGuid(),
            RegisteredAt = DateTime.UtcNow
        });

        return user.Entity;
    }

    public async Task<User> UpdateUsernameAsync(Guid userId, string username, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        user.Username = username;

        return user;
    }

    public async Task<User> UpdateEmailAsync(Guid userId, string email, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        user.Email = email;

        return user;
    }

    public async Task<User> UpdatePasswordAsync(Guid userId, byte[] passwordHash, byte[] passwordSalt, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return user;
    }

    public async Task<User> UpdateUserAsync(Guid userId, string username, string email, byte[] passwordHash, byte[] passwordSalt,
        bool isEmailConfirmed, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        user.Username = username;
        user.Email = email;
        user.IsEmailConfirmed = isEmailConfirmed;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return user;
    }

    public async Task<User> ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        user.IsEmailConfirmed = true;

        return user;
    }

    public async Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        _context.Users.Remove(user);

        return true;
    }

    public async Task<List<User>> GetUsersAsync(int offset, int limit, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return users;
    }

    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FindAsync(userId, cancellationToken);

        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user;
    }
}