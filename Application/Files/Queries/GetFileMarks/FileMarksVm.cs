using Application.Common.Mapping;
using AutoMapper;
using File = Application.Models.File;

namespace Application.Files.Queries.GetFileMarks;

public record FileMarksVm : IMapWith<File>
{
    public Guid Id { get; init; }
    public bool ToAutoDelete { get; init; }
    public bool RemovalMark { get; init; }
    public DateTime RemovalMarkDate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<File, FileMarksVm>();
    }
}