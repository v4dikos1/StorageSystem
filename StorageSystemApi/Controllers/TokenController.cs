using System.Security.Claims;
using Application.Users.Commands.Login;
using Application.Users.Commands.RefreshToken;
using Application.Users.Commands.RevokeToken;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageSystemApi.Models.TokenModels;

namespace StorageSystemApi.Controllers;

[ApiController]
[Route("token/{version:apiVersion}")]
public class TokenController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TokenController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <remarks>
    /// Request example:
    /// api/1.0/token/refresh
    /// {
    ///     "AccessToken": **access token**,
    ///     "RefreshToken": **refresh token** 
    /// }
    /// </remarks>
    /// <param name="request">Access and refresh token</param>
    /// <returns>Returns a new access/refresh token pair</returns>
    /// <response code="200">Token refreshed successfully</response>
    /// <response code="400">Validation errors</response>
    [HttpPost]
    [Route("refresh")]
    [ProducesResponseType( typeof(LoginResponseVm),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationException), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponseVm>> Refresh(TokenRefreshDto request)
    {
        var command = _mapper.Map<RefreshTokenCommand>(request);

        var response = await _mediator.Send(command);

        return Ok(response);
    }


    /// <summary>
    /// Revoke a refresh token
    /// </summary>
    /// <remarks>
    /// Request example:
    /// api/1.0/token/revoke
    /// Authorization: Bearer **access token**
    /// </remarks>
    /// <returns></returns>
    /// <response code="204">Token revoked</response>
    /// <response code="400">Validation errors</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("revoke")]
    public async Task<ActionResult> Revoke()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type is ClaimTypes.NameIdentifier)?.Value;

        if (userId is null) return BadRequest();

        var command = new RevokeTokenCommand(Guid.Parse(userId));

        await _mediator.Send(command);

        return NoContent();
    }
}