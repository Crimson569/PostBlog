using FluentValidation;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using AuthService.Api.Contracts.Responses;
using AuthService.Application.Exceptions;

namespace SupportService.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = new ErrorResponse
        {
            Message = exception.Message
        };

        var errorResponseJson = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });

        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = (int)MapStatusCode(exception);
        await context.Response.WriteAsync(errorResponseJson);
    }

    private static HttpStatusCode MapStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentException _ => HttpStatusCode.BadRequest,
            FluentValidation.ValidationException _ => HttpStatusCode.BadRequest,
            System.ComponentModel.DataAnnotations.ValidationException _ => HttpStatusCode.BadRequest,
            NotFoundException _ => HttpStatusCode.NotFound,
            AlreadyExistsException _ => HttpStatusCode.Conflict,
            AppException appEx => (HttpStatusCode)appEx.StatusCode,
            _ => HttpStatusCode.InternalServerError
        };
    }

}