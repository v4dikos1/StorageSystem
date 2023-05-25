using System.Security.Claims;
using Application.Files.Commands.DeleteFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Files.Commands.UploadFile;
using Application.Files.Queries.GetFile;
using Application.Files.Queries.GetFileInfo;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using StorageSystemApi.Models.Files;
using Microsoft.AspNetCore.SignalR;
using StorageSystemApi.Hubs;
using FileInfo = Application.Files.Queries.GetFileInfo.FileInfo;

namespace StorageSystemApi.Controllers;

[ApiController]
[Route("api/{version:apiVersion}/files")]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IHubContext<FileHub, IFileHub> _hubContext;

    public FileController(IMediator mediator, IMapper mapper, IHubContext<FileHub, IFileHub> hubContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    /// <summary>
    /// Upload a file
    /// </summary>
    /// <param name="request">File, File metadata</param>
    /// <returns>code 201</returns>
    /// <response code="201">File uploaded</response>
    /// <response code="400">Validation exceptions</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UploadFile([FromForm] UploadFileDto request)
    {
        var command = _mapper.Map<UploadFileCommand>(request);

        var fileId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetFileInfo), new { id = fileId });
    }

    /// <summary>
    /// Get file metadata
    /// </summary>
    /// <remarks>
    /// Request example:
    /// GET api/1.0/files?id=b897c81c-c54b-43c5-8bef-9375b7223916
    /// </remarks>
    /// <param name="id">file id</param>
    /// <returns>file metadata</returns>
    /// <response code="200"></response>
    /// <response code="400">Validation exceptions</response>
    [HttpGet(Name = "GetFileInfo")]
    [ProducesResponseType(typeof(FileInfo),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FileInfo>> GetFileInfo(Guid id)
    {
        var response = await _mediator.Send(new GetFileInfoQuery(id));

        return Ok(response);
    }

    /// <summary>
    /// Get a file
    /// </summary>
    /// <remarks>
    /// Request example:
    /// GET api/1.0/files/b897c81c-c54b-43c5-8bef-9375b7223916
    /// </remarks>
    /// <param name="fileId">file id</param>
    /// <returns>A file stream</returns>
    /// <response code="400">Validation exceptions</response>
    [HttpGet("{fileId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DownloadFile(Guid fileId)
    {
        var response = await _mediator.Send(new GetFileQuery(fileId));

        await _hubContext.Clients.All.FileDownloadStarted(response.FileId, response.ToDelete);

        return File(response.FileStream, "application/octet-stream", response.FileName);
    }

    /// <summary>
    /// Delete a file
    /// </summary>
    /// <remarks>
    /// You need authorization to delete a file.
    /// Request example:
    /// DELETE api/1.0/files?id=b897c81c-c54b-43c5-8bef-9375b7223916
    /// </remarks>
    /// <param name="id">file id</param>
    /// <returns></returns>
    /// <response code="204">File deleted</response>
    /// <response code="400">Validation exceptions</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Users can only delete their own files</response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteFile(Guid id)
    {
        var userId = User.Claims.First(c => c.Type is ClaimTypes.NameIdentifier).Value;

        await _mediator.Send(new DeleteFileCommand(id, Guid.Parse(userId)));
        return NoContent();
    }

}