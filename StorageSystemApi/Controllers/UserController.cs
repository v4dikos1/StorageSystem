using System.Security.Claims;
using Application.Files.Queries.GetFiles;
using Application.Users.Commands.ConfirmEmail;
using Application.Users.Commands.Login;
using Application.Users.Commands.Registration;
using Application.Users.Queries.GetUserProfile;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageSystemApi.Models.UserModels;

namespace StorageSystemApi.Controllers;

[ApiController]
[Route("api/{version:apiVersion}/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Register a user
    /// </summary>
    /// <remarks>
    /// Request example:
    /// POST /api/1.0/users
    /// {
    ///     "Username": "v4dikos",
    ///     "Email": "v4dikos@yandex.ru",
    ///     "Password": "12345"
    /// }
    /// </remarks>
    /// <param name="request">Username, Email, Password</param>
    /// <returns>code 201</returns>
    /// <response code="201">User registered successfully</response>
    /// <response code="400">Validation exceptions</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateUser([FromForm] UserRegistrationDto request)
    {
        var command = _mapper.Map<RegistrationCommand>(request);

        var user = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetUser), new { id = user }, user);
    }


    /// <summary>
    /// Authentication/Authorization
    /// </summary>
    /// <remarks>
    /// Request example:
    /// POST api/1.0/users/user
    /// {
    ///     "Login": "v4dikos",
    ///     "Password": "12345"
    /// }
    /// </remarks>
    /// <param name="request">Login (email) and Password</param>
    /// <returns>Returns access-token</returns>
    /// <response code="200">Authorized</response>
    /// <response code="401">Not authorized</response>
    /// <response code="403">Need to confirm the email</response>
    /// <response code="404">User not found</response>
    [HttpPost("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> Auth([FromBody] UserLoginDto request)
    {
        var command = _mapper.Map<LoginCommand>(request);

        var token = await _mediator.Send(command);

        return Ok(token);
    }



    /// <summary>
    /// Get the user
    /// </summary>
    /// <remarks>
    /// Request example:
    /// GET api/1.0/users/b897c81c-c54b-43c5-8bef-9375b7223916
    /// </remarks>
    /// <param name="id">User id</param>
    /// <returns>Returns user view model (UserProfileVm)</returns>
    /// <response code="200">User found</response>
    /// <response code="404">User not found</response>
    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserProfileVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserProfileVm>> GetUser(Guid id)
    {
        var query = new GetUserProfileQuery(id);

        var response = await _mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Verify the email
    /// </summary>
    /// <param name="verificationToken">Token sent in the email</param>
    /// <returns></returns>
    /// <response code="204">Email verified</response>
    /// <resposne code="400">Incorrect code</resposne>
    [HttpGet]
    [Route("verify")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> VerifyEmail(string verificationToken)
    {
        var command = new ConfirmEmailCommand(verificationToken);

        await _mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Get a list of files for an authorized user
    /// </summary>
    /// <remarks>
    /// Request example:
    /// GET /api/1.0/users/files?offset=5&amp;limit=10
    /// </remarks>
    /// <param name="offset">Offset from the beginning</param>
    /// <param name="limit">files limit</param>
    /// <returns>file list</returns>
    /// <response code="200"></response>
    /// <resposne code="400">Validation errors</resposne>
    /// <response code="401">Not authorized</response>
    [HttpGet]
    [Route("files")]
    [Authorize]
    [ProducesResponseType(typeof(FileListVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FileListVm>> GetFiles(int offset, int limit)
    {
        var userId = Guid.Parse(User.Claims.First(c => c.Type is ClaimTypes.NameIdentifier).Value);

        var query = new GetFilesQuery(userId, offset, limit);

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}