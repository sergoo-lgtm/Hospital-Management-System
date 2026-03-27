namespace HospitalManagementSystemAPIVersion.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler : IExceptionHandler
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
        _logger.LogError(exception, exception.Message);

        var response = context.Response;

        response.ContentType = "application/json";

        switch (exception)
        {
            case NotFoundException:
            case KeyNotFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                break;

            case BusinessException:
            case ArgumentException:
                response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await response.WriteAsJsonAsync(new
        {
            message = exception.Message
        });

        return true;
    }
}