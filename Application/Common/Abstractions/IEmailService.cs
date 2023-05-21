namespace Application.Common.Abstractions;

public interface IEmailService
{
    Task SendEmailConfirmationAsync(string receiverEmail);
}