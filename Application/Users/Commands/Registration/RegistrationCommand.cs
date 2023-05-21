using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.Registration;

public record RegistrationCommand(Email Email, Username Username, string Password) : IRequest<Guid>{}