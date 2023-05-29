using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class FileManager
{
    private readonly List<User> _users;
    public IReadOnlyCollection<User> Users => _users;

    public FileManager()
    {
        _users = new List<User>();
    }

    /// <summary>
    /// Register a user
    /// </summary>
    /// <param name="email">user email</param>
    /// <param name="username">username</param>
    /// <returns></returns>
    public bool RegisterUser(string email, string username)
    {
        try
        {
            if (!(Users.FirstOrDefault(u => u.Email.Value == email) is null)) return false;

            _users.Add(
                new User(
                    Guid.NewGuid(),
                    new Username(username),
                    new Email(email))
            );

            return true;
        }
        catch (ArgumentException e)
        {
            return false;
        }
    }

    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="file">A file</param>
    /// <param name="userId">user id</param>
    public void CreateFile(File file, Guid userId)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        user.AddFile(file);
    }

    /// <summary>
    /// Delete a file
    /// </summary>
    /// <param name="fileId">file id</param>
    /// <returns>true if file was found and removed</returns>
    public bool DeleteFile(Guid fileId)
    {
        var user = Users.FirstOrDefault(
            u => u.Files.FirstOrDefault(
                f => f.Id == fileId) is not null);

        if (user != null)
        {
            user.RemoveFile(fileId);

            return true;
        }

        return false;
    }
}