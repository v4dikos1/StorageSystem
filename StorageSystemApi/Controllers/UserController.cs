using Application.Users.Commands.ConfirmEmail;
using Application.Users.Commands.Login;
using Application.Users.Commands.Registration;
using Application.Users.Queries.GetUserProfile;
using AutoMapper;
using MediatR;
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
    /// <param name="verificationCode">Code sent in the mail</param>
    /// <returns></returns>
    /// <response code="204">Email verified</response>
    /// <resposne code="400">Incorrect code</resposne>
    [HttpPatch]
    [Route("verify")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> VerifyEmail(string verificationCode)
    {
        var command = new ConfirmEmailCommand(verificationCode);

        await _mediator.Send(command);

        return NoContent();
    }
}