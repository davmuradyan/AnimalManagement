using AnimalService.Application.Services.AnimalServices;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using AnimalService.Infrastructure.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalService.Tests.IntegrationTests.Tests;

public class AnimalIntegrationTests(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task Create_ValidRequest_ShouldCreateAnimal()
    {
        // Arrange
        using var scope = factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        var service = scope.ServiceProvider.GetRequiredService<IAnimalService>();
        var request = new CreateAnimalRequest(Name: "Cat", Age: 3, Weight: 5, Gender: 1, Color: 3);

        // Act
        var result = await service.Create(request);

        // Assert - returned DTO
        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Age, result.Age);
        Assert.Equal(request.Weight, result.Weight);

        // Assert - database state
        var animal = await dbContext.Animals.FirstOrDefaultAsync(a => a.Name == request.Name);

        Assert.NotNull(animal);
        Assert.Equal(request.Age, animal.Age);
        Assert.Equal(request.Weight, animal.Weight);
    }
}