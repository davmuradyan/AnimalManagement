using AnimalManagement.Shared.Configs;

namespace UserService.Domain.Configs;

public class Configuration : BaseConfiguration 
{
    public Jwt Jwt { get; set; } = new Jwt();
}