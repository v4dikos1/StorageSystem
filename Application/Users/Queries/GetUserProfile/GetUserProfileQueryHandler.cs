using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetUserProfile;

internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileVm> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
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