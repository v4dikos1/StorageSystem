using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using FluentValidation;
using MediatR;
using System.Security.Claims;

namespace Application.Users.Commands.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUserRepository userRepository,
        IPasswordService passwordService,
        ITokenService tokenService,
        IValidator<LoginCommand> validator, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseVm> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Searching for a user by email
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        // Checking if the email confirmed
        if (!user.IsEmailConfirmed)
        {
            throw new EmailConfirmationRequiredException();
        }

        // Checking if the password is the same as the one stored in the database
        if (!_passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidLoginOrPasswordException();
        }

        // Creating user claims (email, id) 
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        // Generating access token
        var accessToken = _tokenService.Create(claims);

        // Generating refresh token
        var refreshToken = _tokenService.CreateRefreshToken();

        // Updating refresh token
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponseVm(accessToken, refreshToken);
    }
}