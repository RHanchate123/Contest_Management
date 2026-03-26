using Contest_Management.API.Models;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Contest_Management.API.Middleware 
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;  // Use Serilog's ILogger

        // Change the constructor to inject Serilog's logger
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = Log.ForContext<ExceptionHandlingMiddleware>() ?? throw new ArgumentNullException("Serilog"); // Creates a logger for this class;  // Use the injected Serilog logger
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            // Default error response
            var errorResponse = new ErrorDetailsModel
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Noe uventet har skjedd, vennligst prøv igjen senere."
            };

            switch (exception)
            {
                case Exceptions.APIBusinessExceptions apiEx:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Message = apiEx.Message;
                    _logger.Warning(exception, "Business logic exception occurred.");  
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server error";
                    _logger.Error(exception, exception.Message);  
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
