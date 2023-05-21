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

    public DateTime CreatedAt { get; set; }

    // The document creator
    public required Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public bool ToAutoDelete { get; set; } = false;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<File, Domain.Entities.File>();
    }
}