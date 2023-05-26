using Application.Common.Mapping;
using AutoMapper;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using File = Application.Models.File;

namespace Application.Files.Queries.GetFileInfo;

/// <summary>
/// A model that represents information about the file
/// </summary>
public class FileInfo : IMapWith<File>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = Filename.DefaultValue;
    public string Description { get; set; } = FileDescription.DefaultValue;

    // Download link
    public string Url { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public required Guid OwnerId { get; set; }

    public bool ToAutoDelete { get; set; } = false;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<File, FileInfo>()
            .ForMember(f => f.Url, opt =>
                opt.MapFrom((src, dest, destMember, context)
                    => context.Items["ReceiptLink"] + src.Id.ToString()));
    }
}