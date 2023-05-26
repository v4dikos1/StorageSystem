using Application.Common.Abstractions;
using FluentValidation;
using MediatR;

namespace Application.Users.Commands.ConfirmEmail;

internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ConfirmEmailAsync(request.VerificationToken, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}