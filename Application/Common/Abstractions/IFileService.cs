using Microsoft.AspNetCore.Http;

namespace Application.Common.Abstractions;

/// <summary>
/// A file management service
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Upload a file
    /// </summary>
    /// <param name="objectName">File name to be used as a filename in the storage </param>
    /// <param name="file">A file</param>
    /// <returns>true if file uploaded successfully</returns>
    public Task<bool> UploadFileAsync(string objectName, IFormFile file);

    /// <summary>
    /// Get a file by id
    /// </summary>
    /// <param name="fileName">file name</param>
    /// <returns>file stream</returns>
    public Task<Stream> GetFileAsync(string fileName);

    /// <summary>
    /// Delete a file by id
    /// </summary>
    /// <param name="fileName">file name</param>
    /// <returns>true if file deleted successfully</returns>
    public Task<bool> DeleteFileAsync(string fileName);
}