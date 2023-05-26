using Application.Common.Abstractions;
using FluentValidation;
using MediatR;

namespace Application.Users.Commands.ConfirmEmail;

internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ConfirmEmailCommand> _validator;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<ConfirmEmailCommand> validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        await _userRepository.ConfirmEmailAsync(request.VerificationToken, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}