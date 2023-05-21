using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Application.Models.File;

namespace Persistence.EntityTypeConfiguration;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("File");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name).IsRequired();
        builder.Property(f => f.CreatedAt).IsRequired();
        builder.Property(f => f.ToAutoDelete).IsRequired().HasDefaultValue("false");
        builder.Property(f => f.Path).IsRequired();

        builder
            .HasOne<User>(f => f.Owner)
            .WithMany(u => u.UploadedFiles);

    }
}