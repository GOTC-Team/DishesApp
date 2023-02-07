using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using FluentValidation;
using Notes.Application.Common.Excpetions;

namespace Notes.API.CustomMiddlewares;

/// <summary>
/// Custom middleware for handling exceptions
/// Injected: request delegate
/// </summary>
public class CustomExceptionHandler
{
    public CustomExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }
    
    private readonly RequestDelegate _next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await Handle(context, e);
        }
    }

    private Task Handle(HttpContext context, Exception occured)
    {
        var responseCode = HttpStatusCode.InternalServerError;
        var responseMessage = string.Empty;
        
        switch (occured)
        {
            case ValidationException validationException:
                responseCode = HttpStatusCode.BadRequest;
                responseMessage = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                responseCode = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)responseCode;

        if (responseMessage == string.Empty)
        {
            responseMessage = JsonSerializer.Serialize(new { midlewareHandledErrorMessage = occured.Message });
        }

        return context.Response.WriteAsync(responseMessage);
    }
}