namespace StorageSystemApi.Middleware;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    /// <summary>
    /// An extension method for applying the custom intermediate level.
    /// </summary>
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}