using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler 
        (ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message {exceptionMessage}, Time of occurence {time}", exception.Message, DateTime.UtcNow);            

            (string Details, string Title, string StatusCode) details = exception switch
            {
                NotFoundException notFoundException => 
                (
                    notFoundException.Message,
                    notFoundException.GetType().Name, 
                    StatusCodes.Status404NotFound.ToString()
                ),
                BadRequestException badRequestException => (
                    badRequestException.Message,
                    badRequestException.GetType().Name,
                    StatusCodes.Status400BadRequest.ToString()
                ),
                InternalServerException internalServerException => (
                    internalServerException.Message,
                    internalServerException.GetType().Name,
                    StatusCodes.Status500InternalServerError.ToString()
                ),
                ValidationException validationException => (
                    validationException.Message,
                    validationException.GetType().Name,
                    StatusCodes.Status400BadRequest.ToString()
                ),
                _ => 
                (
                    exception.Message, 
                    "Internal Server Error", 
                    StatusCodes.Status500InternalServerError.ToString()
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Details,
                Status = int.Parse(details.StatusCode)
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            
            if(exception is ValidationException validationEx)
            {
                problemDetails.Extensions.Add("validationErrors", validationEx.ValidationResult);
            }
            
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
