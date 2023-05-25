using Application.Files.Commands.DeleteFile;
using Application.Files.Queries.GetFileMarks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace StorageSystemApi.Hubs;

/// <summary>
/// A hub for tracking file downloads
/// </summary>
[Authorize]
public class FileHub : Hub<IFileHub>
{
    private readonly IMediator _mediator;

    public FileHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// This method is called by the client when the requested file has been downloaded.
    /// If the requested file was marked for deletion, it will be deleted.
    /// This is necessary in order to be able to delete files safely,
    /// namely to know if the connection was interrupted while the file was being downloaded and if it should be deleted.
    /// 
    /// Probably not the best solution to the problem.
    /// TODO: come up with another solution
    /// </summary>
    /// <param name="fileId">file id</param>
    /// <param name="userId">user id</param>
    /// <returns></returns>
    public async Task FileDownloaded(string fileId, string userId)
    {
        var fileMarks = await _mediator.Send(new GetFileMarksQuery(Guid.Parse(fileId)));

        if (fileMarks.RemovalMark)
        {
            await _mediator.Send(new DeleteFileCommand(Guid.Parse(fileId), Guid.Parse(userId)));
        }
    }
}