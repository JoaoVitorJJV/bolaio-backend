using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UFRA.Bolaio.API.Middlewares
{
    public class GlobalExceptionHandler:IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is DomainException domainException)
            {
                _logger.LogWarning("Erro de validação: {Message}", domainException.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Erro de validação",
                    Detail = domainException.Message,
                    Type = "https:tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }
            return false;
        }
    }
}

