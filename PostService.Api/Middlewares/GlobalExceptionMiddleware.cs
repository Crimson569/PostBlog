using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using PostService.Application.Exceptions;
using PostService.Api.Contracts.Responses;

namespace PostService.Api.Middlewares;

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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = new ErrorResponse()
        {
            Message = exception.Message
        };

        var errorResponseJson = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = (int)MapStatusCode(exception);
        await context.Response.WriteAsync(errorResponseJson);
    }

    private HttpStatusCode MapStatusCode(Exception exception)
    {
        return exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            AppException appEx => (HttpStatusCode)appEx.StatusCode,
            _ => HttpStatusCode.InternalServerError
        };
    }
}