using FluentValidation;

namespace CleanArchitecture.WebApi.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (e.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails()
            {
                StatusCode = 403,
                Errors = ((ValidationException)e).Errors.Select(x => x.PropertyName)

            }.ToString());
        }

        return context.Response.WriteAsync(new ErrorResult()
        {
            Message = e.Message,
            StatusCode = context.Response.StatusCode

        }.ToString());
    }
}
