namespace StorageSystemApi.Hubs;

public interface IFileHub
{
    /// <summary>
    /// A method called at the moment when the client starts downloading a file.
    /// The client on its side monitors the call to this method and takes the necessary action.
    /// When the file download is complete, client must call the "FileDownloaded" method on the client side
    /// to notify the server that the file has been successfully downloaded and can be safely deleted.
    /// This is necessary to safely (without fear of a broken connection while downloading the file, for example)
    /// delete files marked as auto-deleted.
    /// </summary>
    /// <param name="fileId">file Id</param>
    /// <param name="toDelete">Is it necessary to delete the file</param>
    /// <returns></returns>
    Task FileDownloadStarted(Guid  fileId, bool toDelete);
}