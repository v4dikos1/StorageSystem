using Application.Common.Exceptions;
using System.Net;
using System.Text.Json;
using FluentValidation;
using System.Security.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace StorageSystemApi.Middleware;

/// <summary>
/// Middleware for catching custom exceptions
/// </summary>
public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;

            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;

            case AlreadyExistsException:
                code = HttpStatusCode.Conflict;
                break;

            case InvalidLoginOrPasswordException:
                code = HttpStatusCode.Unauthorized;
                break;

            case InvalidCredentialException:
                code = HttpStatusCode.BadRequest;
                break;

            case SecurityTokenException:
                code = HttpStatusCode.BadRequest;
                break;

            case EmailConfirmationRequiredException:
                code = HttpStatusCode.Forbidden;
                break;

            case WrongConfirmationCodeException:
                code = HttpStatusCode.BadRequest;
                break;

            case IllegalOperationException:
                code = HttpStatusCode.Forbidden;
                break;

            case ArgumentException:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            Console.WriteLine(exception);
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}