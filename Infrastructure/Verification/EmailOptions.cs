namespace Infrastructure.Verification;

/// <summary>
/// Data class for the content of the information needed to send the letter
/// </summary>
public class EmailOptions
{
    public required string ConfirmUrl { get; init; }

    public required string SenderName { get; init; }
    public required string SenderEmail { get; init; }

    public required string SmtpHost { get; init; }
    public required string SmtpPort { get; init; }

    public required string SmtpUsername { get; init; }
    public required string SmtpServicePassword { get; init; }

}