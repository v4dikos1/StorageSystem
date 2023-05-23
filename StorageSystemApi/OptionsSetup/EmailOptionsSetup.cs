using Infrastructure.Verification;
using Microsoft.Extensions.Options;

namespace StorageSystemApi.OptionsSetup;

public class EmailOptionsSetup: IConfigureOptions<EmailOptions>
{
    private const string SectionName = "EmailOptions";
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EmailOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}