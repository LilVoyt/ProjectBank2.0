using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProjectBank.Presentation.ExceptionHandling
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            if (exception is FluentValidation.ValidationException validationException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = response.StatusCode;
                problemDetails.Title = "Validation failed.";
                problemDetails.Detail = "One or more validation errors occurred.";

                problemDetails.Extensions["errors"] = validationException.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else if (exception is UnauthorizedAccessException)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                problemDetails.Status = response.StatusCode;
                problemDetails.Title = "Unauthorized.";
                problemDetails.Detail = "Access is denied.";
            }
            else if (exception is KeyNotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Status = response.StatusCode;
                problemDetails.Title = "Not Found.";
                problemDetails.Detail = "The requested resource was not found.";
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Status = response.StatusCode;
                problemDetails.Title = "Internal Server Error.";
                problemDetails.Detail = "An unexpected error occurred.";
            }

            _logger.LogError(exception, "Handled exception: {Title}", problemDetails.Title);

            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
