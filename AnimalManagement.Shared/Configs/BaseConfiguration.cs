namespace AnimalManagement.Shared.Configs;

public abstract class BaseConfiguration
{
    public Logging Logging { get; set; }  = new();
    public ConnectionStrings ConnectionStrings { get; set; } = new();
}

public class ConnectionStrings
{
    public string MainConnectionString { get; set; } = string.Empty;
}

public class Logging
{
    public LogLevel LogLevel { get; set; } = new();
}

public class LogLevel
{
    public string Default { get; set; } = string.Empty;
}