using AnimalManagement.Shared.Logging;
using AnimalService.Application.ExtensionMethods;
using AnimalService.Infrastructure.DAL.Data;
using AnimalService.Infrastructure.ExtensionMethods;
using AnimalService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();

builder.Logging.AddProvider(
    new FileLoggerProvider(
        Path.Combine(
            "C:\\Users\\davitm\\Desktop\\ProjectB\\Logs",
            "Logs",
            "animal-service-logs.txt")));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var initializer = scope.ServiceProvider
        .GetRequiredService<AnimalDataInitializer>();

    await initializer.InitializeAsync();
}

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
