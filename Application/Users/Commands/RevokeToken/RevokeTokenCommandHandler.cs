using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using FluentValidation;
using MediatR;

namespace Application.Users.Commands.RevokeToken;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RevokeTokenCommand> _validator;

    public RevokeTokenCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<RevokeTokenCommand> validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Searching for the user
        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        // Revoking refresh token
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}