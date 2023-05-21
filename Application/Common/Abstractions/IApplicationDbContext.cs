using Application.Models;
using Microsoft.EntityFrameworkCore;
using File = Application.Models.File;

namespace Application.Common.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<File> Files { get; set; }
}