﻿using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// A file entity created to encapsulate the business logic associated with users.
/// However, in this case, it does not contain any logic and is only used for now to save the architecture style
/// and the possibility to disperse/use it in the future. 
/// </summary>
public class User : Entity
{
    private readonly List<File> _files = new();

    public User(Guid id, Username username, Email email) : base(id)
    {
        Username = username;
        Email = email;
    }

    /// <summary>
    /// Username
    /// </summary>
    public Username Username { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public Email Email { get; init; }

    /// <summary>
    /// Uploaded files
    /// </summary>
    public IReadOnlyCollection<File> Files => _files;

    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="file"></param>
    public void AddFile(File file)
    {
        var fileExists = _files.FirstOrDefault(f => f.Id == file.Id);
        if (fileExists is null)
        {
            _files.Add(file);
        }
    }

    public void RemoveFile(Guid fileId)
    {
        var file = _files.First(f => f.Id == fileId);
        _files.Remove(file);
    }
}