namespace Application.Files;

/// <summary>
/// Model for representing storage options
/// </summary>
public record FileStorageOptions
{
    /// <summary>
    /// Url of the service that provides s3 storage services
    /// </summary>
    public required string ServiceUrl { get; init; }

    /// <summary>
    /// Name of the bucket in which objects are stored
    /// </summary>
    public required string BucketName { get; init; }
        
    /// <summary>
    /// An endpoint at which you can download the file
    /// </summary>
    public required string ReceiptLink { get; init;}
}