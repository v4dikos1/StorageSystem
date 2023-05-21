using Application.Common.Abstractions;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;
using File = Application.Models.File;

namespace Persistence;

public sealed class StorageServiceDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<File> Files { get; set; } = null!;

    public StorageServiceDbContext(DbContextOptions<StorageServiceDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
    }
}