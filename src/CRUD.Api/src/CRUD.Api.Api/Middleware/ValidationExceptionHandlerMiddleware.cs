using System.Net;
using FluentValidation;

namespace CRUD.Api.Api.Middleware;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            
            var errors =
                ex
                    .Errors
                    .GroupBy(
                        x => x.PropertyName,
                        x => x.ErrorMessage,
                        (propertyName, errorMessages) => new
                        {
                            Key = propertyName,
                            Values = errorMessages.Distinct().ToArray()
                        })
                    .ToDictionary(x => x.Key, x => x.Values);

            var response = new
            {
                title = "Validation Failure",
                status = StatusCodes.Status422UnprocessableEntity,
                detail = "One or more validation errors occurred",
                errors
            };

            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}