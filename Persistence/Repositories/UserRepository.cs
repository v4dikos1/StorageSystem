using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using File = Application.Models.File;
using User = Application.Models.User;

namespace Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
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

    public async Task<User> ConfirmEmailAsync(string verificationToken, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.VerificationToken == verificationToken, cancellationToken);

        if (user is null)
        {
            throw new WrongConfirmationCodeException();
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

    public async Task<List<File>> GetUserFilesAsync(Guid userId, int offset, int limit, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UploadedFiles)
            .Skip(offset)
            .Take(limit)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        return user.UploadedFiles;
    }
}