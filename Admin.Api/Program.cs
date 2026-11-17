using System.Text;
using Admin.Api.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
    .AddReverseProxy().LoadFromMemory(routes, clusters);

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