using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using MediatR;

namespace Application.Users.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Searching for a user by email
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        // Checking if the password is the same as the one stored in the database
        if (!_passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidLoginOrPasswordException();
        }

        // Generating jwt-token
        var token = _tokenService.Create(user);

        return token;
    }
}