using System.Net;
using System.Text.Json;

namespace Cancellation.Service.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning("Request timeout - CancellationToken triggered");
                await HandleExceptionAsync(context, ex, HttpStatusCode.RequestTimeout, "Request timeout");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = message,
                details = exception.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
