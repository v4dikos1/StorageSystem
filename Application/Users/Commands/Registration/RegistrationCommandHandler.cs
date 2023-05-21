using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using MediatR;

namespace Application.Users.Commands.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public RegistrationCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Guid> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        // Check if a user with the same email already exists
        if (await _userRepository.GetUserByEmailAsync(request.Email.Value, cancellationToken) is not null)
        {
            throw new AlreadyExistsException(nameof(User), request.Email.Value);
        }

        // Hashing the password
        _passwordService.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = _userRepository.CreateUser(
            request.Username.Value,
            request.Email.Value,
            passwordHash,
            passwordSalt,
            false);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}