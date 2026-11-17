using AnimalManagement.Shared.Configs;

namespace AnimalService.Domain.Configs;

public class Configuration : BaseConfiguration
{
    public Services Services { get; set; } = new();
}