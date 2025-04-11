using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MiniProjet.ProjectManagement.API;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        return await HandleExceptionAsync(httpContext, exception, cancellationToken);
    }

    public async Task<bool> HandleExceptionAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken = default)
    {
        // Log the exception
        _logger.LogError(exception, "An unhandled exception occurred.");

        // Set the response status code and content type
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        // Create a standardized error response
        var errorResponse = new
        {
            Message = "An unexpected error occurred. Please try again later.",
            Details = exception.Message // Avoid exposing sensitive details in production
        };

        // Serialize the error response to JSON
        var errorJson = JsonSerializer.Serialize(errorResponse);

        // Write the response
        await httpContext.Response.WriteAsync(errorJson, cancellationToken);

        // Indicate that the exception was handled
        return true;
    }
}
