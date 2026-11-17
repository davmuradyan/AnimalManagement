using Yarp.ReverseProxy.Configuration;

namespace Admin.Api.Configs.ReserveProxy;

public record ReverseProxy
{
    public Dictionary<string, RouteConfig> Routes { get; set; } = new();
    public Dictionary<string, ClusterConfig> Clusters { get; set; } = new();
}

public record Route
{
    public string ClusterId { get; set; } = string.Empty;
    public string AuthorizationPolicy { get; set; } = string.Empty;
    public Match Match { get; set; } = new();
}

public record Match
{
    public string Path { get; set; } = string.Empty;
    public string[] Methods { get; set; } = [];
}

public record Cluster
{
    public Dictionary<string, Destination> Destinations { get; set; } = new();
}

public record Destination
{
    public string Address { get; set; } = string.Empty;
}