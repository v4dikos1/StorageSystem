using MediatR;

namespace Application.Users.Queries.GetUserProfile;

/// <summary>
/// A query to retrieve information about a user by Id
/// </summary>
/// <param name="Id">User id</param>
/// <returns>Representative information about the user</returns>
public record GetUserProfileQuery(Guid Id): IRequest<UserProfileVm>{}