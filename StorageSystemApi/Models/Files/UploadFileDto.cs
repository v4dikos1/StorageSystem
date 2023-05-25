using Application.Common.Mapping;
using Application.Files.Commands.UploadFile;
using AutoMapper;

namespace StorageSystemApi.Models.Files;

public record UploadFileDto : IMapWith<UploadFileCommand>
{
    /// <summary>
    /// File name
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Creator id
    /// </summary>
    public required Guid CreatorId { get; init; }

    /// <summary>
    /// File description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Whether to delete a file when downloading
    /// </summary>
    public bool? ToAutoDelete { get; init; }

    /// <summary>
    /// A file
    /// </summary>
    public required IFormFile File { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UploadFileDto, UploadFileCommand>();
    }
}