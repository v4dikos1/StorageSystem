using Application.Files;
using Microsoft.Extensions.Options;

namespace StorageSystemApi.OptionsSetup;

public class StorageOptionsSetup : IConfigureOptions<FileStorageOptions>
{
    private const string SectionName = "s3StorageOptions";
    private readonly IConfiguration _configuration;

    public StorageOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(FileStorageOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}