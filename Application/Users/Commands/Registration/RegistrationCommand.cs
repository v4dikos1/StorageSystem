using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.Registration;

/// <summary>
/// A command worth calling to register a user
/// </summary>
/// <param name="Email">Unique user email</param>
/// <param name="Username">Username</param>
/// <param name="Password">Password</param>
/// <returns>User id</returns>
public record RegistrationCommand(Email Email, Username Username, string Password) : IRequest<Guid>{}