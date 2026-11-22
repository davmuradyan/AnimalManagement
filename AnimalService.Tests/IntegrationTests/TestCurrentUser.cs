using AnimalService.Application.Interfaces;

namespace AnimalService.Tests.IntegrationTests;

public class TestCurrentUser : ICurrentUser
{
    public string UserId => "1";
    public string Username => "TestUser";
}