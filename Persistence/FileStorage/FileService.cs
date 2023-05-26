using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Common.Abstractions;
using Application.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Persistence.FileStorage;

internal class FileService : IFileService
{
    private readonly FileStorageOptions _options;
    private readonly IAmazonS3 _s3Client;

    public FileService(IOptions<FileStorageOptions> options, IAmazonS3 s3Client)
    {
        this._s3Client = s3Client;
        _options = options.Value;
    }

    public async Task<bool> UploadFileAsync(string objectName, IFormFile file)
    {
        using (var newMemoryStream = new MemoryStream())
        {
            await file.CopyToAsync(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = objectName,
                BucketName = _options.BucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return true;
        }
    }

    public async Task<Stream> GetFileAsync(string fileName)
    {
        var request = new GetObjectRequest
        {
            BucketName = _options.BucketName,
            Key = fileName
        };

        var response = await _s3Client.GetObjectAsync(request);
        return response.ResponseStream;
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _options.BucketName,
            Key = fileName
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);

        return true;
    }
}