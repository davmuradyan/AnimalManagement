using AnimalManagement.Shared.Logging;
using UserService.Api.Middlewares;
using UserService.Application.ExtensionMethods;
using UserService.Domain.Configs;
using UserService.Infrastructure.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<Configuration>();

if (configuration is null)
{
    Console.Error.WriteLine("No configuration found");
    return;
}

if (!Enum.TryParse<LogLevel>(configuration.Logging.LogLevel.Default, out var defaultLogLevel))
{
    Console.Error.WriteLine($"Invalid logging level: {defaultLogLevel}");
    return;
}

builder.Logging.ClearProviders();

builder.Logging.AddProvider(
    new FileLoggerProvider(
        Path.Combine(
            "C:\\Users\\davitm\\Desktop\\ProjectB\\Logs",
            "Logs",
            "user-service-logs.txt"), defaultLogLevel));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();