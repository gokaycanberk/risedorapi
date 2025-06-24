using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RisedorApi.Shared.Common;
using RisedorApi.Shared.Exceptions;

namespace RisedorApi.Shared.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = exception switch
        {
            ApiException apiException
                => new ErrorResponse(apiException.Message) { StatusCode = apiException.StatusCode },
            ValidationException validationException
                => new ErrorResponse("Validation error")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = validationException.Errors.Select(e => e.ErrorMessage)
                },
            _
                => new ErrorResponse("An unexpected error occurred")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                }
        };

        response.StatusCode = errorResponse.StatusCode;
        var result = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(result);
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
