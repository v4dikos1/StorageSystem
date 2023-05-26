using System.Security.Authentication;
using System.Security.Claims;
using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using Application.Users.Commands.Login;
using FluentValidation;
using MediatR;

namespace Application.Users.Commands.RefreshToken;

internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponseVm>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(ITokenService tokenService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseVm> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Getting principal from token
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        var userId = principal.Claims.FirstOrDefault(c => c.Type is ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            throw new NotFoundException(nameof(User), request.AccessToken);
        }

        // Searching for the user
        var user = await _userRepository.GetUserByIdAsync(Guid.Parse(userId), cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        // Check if the refresh tokens match and if the expiry date hasn't arrived
        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new InvalidCredentialException("Invalid Credentials");
        }

        // Create new tokens
        var newAccessToken = _tokenService.Create(principal.Claims);
        var newRefreshToken = _tokenService.CreateRefreshToken();

        // Update refresh token
        user.RefreshToken = newRefreshToken;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponseVm(newAccessToken, newRefreshToken);
    }
}