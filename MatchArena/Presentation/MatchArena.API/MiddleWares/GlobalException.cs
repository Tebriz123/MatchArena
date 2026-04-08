using MatchArena.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using ApplicationException = MatchArena.Domain.Exceptions.ApplicationException;
namespace MatchArena.API.MiddleWares
{
    public class GlobalException : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, message) = exception switch
            {
                NotFoundException ex => (StatusCodes.Status404NotFound, ex.Message),
                AlreadyExistsException ex => (StatusCodes.Status409Conflict, ex.Message),
                BadRequestException ex => (StatusCodes.Status400BadRequest, ex.Message), 
              _ => (StatusCodes.Status500InternalServerError, exception.Message + " | " + exception.InnerException?.Message)
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(
                new { error = message }, cancellationToken);

            return true;
        }
    }
}
