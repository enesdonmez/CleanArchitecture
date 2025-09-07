using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using FluentValidation;

namespace CleanArchitecture.WebApi.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly AppDbContext _context;

    public ExceptionMiddleware(AppDbContext context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await LogExceptionToDatabaseAsync(e, context.Request);
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

    private async Task LogExceptionToDatabaseAsync(Exception ex,HttpRequest request)
    {
        ErrorLog errorLog = new()
        {
            ErrorMessage = ex.Message,
            StackTrace = ex.StackTrace,
            TimeStamp = DateTime.Now,
            RequestPath = request.Path,
            RequestMethod = request.Method
        };

        await _context.Set<ErrorLog>().AddAsync(errorLog,default);
        await _context.SaveChangesAsync(default);
    }
}
