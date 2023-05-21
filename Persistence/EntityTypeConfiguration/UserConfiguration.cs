using Application.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Application.Models.File;

namespace Persistence.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id).HasName("UserId");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(Username.MaxLength);
        builder.HasIndex(u => u.Username);

        builder.Property(u => u.Email).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.IsEmailConfirmed).IsRequired();
        builder.Property(u => u.RegisteredAt).IsRequired();

        builder.HasMany<File>(u => u.UploadedFiles)
            .WithOne(f => f.Owner);
    }
}