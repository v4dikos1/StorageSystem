using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using FluentValidation;
using MediatR;

namespace Application.Users.Commands.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IEmailService _emailService;
    private readonly IValidator<RegistrationCommand> _validator;


    public RegistrationCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPasswordService passwordService,
        IEmailService emailService,
        IValidator<RegistrationCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _emailService = emailService;
        _validator = validator;
    }

    public async Task<Guid> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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

        // Sending a message with a registration confirmation code
        await _emailService.SendEmailConfirmationAsync(user.Email);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}