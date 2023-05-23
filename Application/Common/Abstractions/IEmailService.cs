namespace Application.Common.Abstractions;

/// <summary>
/// An interface for the service that sends a code to confirm registration by Email
/// </summary>
public interface IEmailService
{
    Task SendEmailAsync(string receiverEmail, string subject, string message);
}