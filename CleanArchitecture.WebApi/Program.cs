using CleanArchitecture.WebApi.Configurations;
using CleanArchitecture.WebApi.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServices(builder.Configuration,typeof(IServiceInstaller).Assembly);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(sc => sc.Layout = ScalarLayout.Classic);
}

app.UseCors();
app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
