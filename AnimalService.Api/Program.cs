using AnimalManagement.Shared.Logging;
using AnimalService.Api.Middlewares;
using AnimalService.Application.ExtensionMethods;
using AnimalService.Domain.Configs;
using AnimalService.Infrastructure.ExtensionMethods;
using Asp.Versioning;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

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

builder.Services.AddSingleton(configuration);

builder.Services.AddHttpContextAccessor();

builder.Logging.ClearProviders();

builder.Logging.AddProvider(
    new FileLoggerProvider(
        Path.Combine(
            "C:\\Users\\davitm\\Desktop\\ProjectB\\Logs",
            "Logs",
            "animal-service-logs.txt"), defaultLogLevel));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddMvc();

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

public partial class Program {}