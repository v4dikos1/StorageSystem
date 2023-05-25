using Amazon.S3;
using Amazon.SQS;
using Application.Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.FileStorage;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    /// <summary>
    /// An extension method for implementing dependencies in the persistence layer
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<StorageServiceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<StorageServiceDbContext>()!);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileRepository, FileRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IFileService, FileService>();

        services.AddDefaultAWSOptions(configuration.GetAWSOptions("AWS"));
        services.AddAWSService<IAmazonS3>();
        services.AddAWSService<IAmazonSQS>();

        return services;
    }
}