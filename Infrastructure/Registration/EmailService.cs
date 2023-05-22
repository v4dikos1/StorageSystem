using Application.Common.Abstractions;

namespace Infrastructure.Registration;

internal class EmailService : IEmailService
{
    public async Task SendEmailConfirmationAsync(string receiverEmail)
    {
        
    }
}