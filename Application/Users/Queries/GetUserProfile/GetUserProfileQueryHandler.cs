using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Users.Queries.GetUserProfile;

internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUserProfileQuery> _validator;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(IUserRepository userRepository, IValidator<GetUserProfileQuery> validator, IMapper mapper)
    {
        _userRepository = userRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<UserProfileVm> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        // Validating
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Checking if the user exists
        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        // Mapping to user view model
        return _mapper.Map<UserProfileVm>(user);
    }
}