using Application.Common.Mapping;
using AutoMapper;
using Domain.ValueObjects;

namespace Application.Models;

public class File : IMapWith<Domain.Entities.File>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = Filename.DefaultValue;
    public string Description { get; set; } = FileDescription.DefaultValue;

    //The path where the document is stored (on the server or in s3 storage)
    public required string Path { get; init; }

    public required string FileNameInStorage { get; set; }

    public DateTime CreatedAt { get; set; }

    // The document creator
    public required Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    // Whether to delete a file when downloading
    public bool ToAutoDelete { get; set; } = false;

    // File removal marker
    public bool RemovalMark {get; set; } = false;
    // Marker date
    public DateTime RemovalMarkDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<File, Domain.Entities.File>();
    }
}