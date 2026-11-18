using System.Text;
using Admin.Api.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<Configuration>();

if (configuration is null)
{
    Console.Error.WriteLine("No configuration found");
    return;
}

var routes = configuration.ReverseProxy.Routes
    .Select(x => x.Value with { RouteId = x.Key })
    .ToList();

var clusters = configuration.ReverseProxy.Clusters
    .Select(x => x.Value with
    {
        ClusterId = x.Key,
        Destinations = x.Value.Destinations
    })
    .ToList();


builder.Services
    .AddReverseProxy().LoadFromMemory(routes, clusters).AddTransforms(builderContext =>
    {
        builderContext.AddRequestTransform(context =>
        {
            var userId = context.HttpContext.User
                .FindFirst("sub")?.Value;

            var username = context.HttpContext.User
                .FindFirst("unique_name")?.Value;

            if (userId is not null)
                context.ProxyRequest.Headers.Add("X-User-Id", userId);

            if (username is not null)
                context.ProxyRequest.Headers.Add("X-Username", username);

            return ValueTask.CompletedTask;
        });
    });

var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<Jwt>()!;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();