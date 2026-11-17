using Admin.Api.Configs.ReserveProxy;
using AnimalManagement.Shared.Configs;

namespace Admin.Api.Configs;

public class Configuration : BaseConfiguration
{
    public Jwt Jwt { get; set; } = new();
    public string AllowedHosts { get; set; } = string.Empty;
    public ReverseProxy ReverseProxy { get; set; } = new();
}