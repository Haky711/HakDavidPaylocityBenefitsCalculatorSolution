using Api.Exceptions;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var result = string.Empty;

            if (exception is NotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            if (exception is NotPossibleToInsertException)
            {
                statusCode = StatusCodes.Status406NotAcceptable; // Not sure if this is the best status code...
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            else
            {
                result = JsonSerializer.Serialize(new { error = "Internal Server Error" });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
