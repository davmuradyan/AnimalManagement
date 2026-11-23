using AnimalManagement.Shared.Enums;
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

        // Assert - result
        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Age, result.Age);
        Assert.Equal(request.Weight, result.Weight);
        Assert.Equal(request.Gender, result.Gender);

        // Assert - database
        var animal = await dbContext.Animals.FirstOrDefaultAsync(a => a.Name == request.Name);

        Assert.NotNull(animal);
        Assert.Equal(request.Age, animal.Age);
        Assert.Equal(request.Weight, animal.Weight);
    }

    [Fact]
    public async Task Update_ValidRequest_ShouldUpdateAnimal()
    {
        // Arrange
        using var scope = factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        var service = scope.ServiceProvider.GetRequiredService<IAnimalService>();
        var dto = await service.Create(new CreateAnimalRequest(Name: "Horse", Age: 3, Weight: 5, Gender: 1, Color: 3));
        var request = new UpdateAnimalRequest(Id: dto.Id, Name: "Dog", Age: 4, Weight: 6, Gender: 2, Color: 1, RowVersion: dto.RowVersion);
        
        // Act
        var result = await service.Update(request);
        
        // Assert - result
        Assert.NotNull(result);
        Assert.Equal(request.Id, result.Id);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Age, result.Age);
        Assert.Equal(request.Weight, result.Weight);
        Assert.Equal(request.Gender, result.Gender);
        Assert.Equal(request.Color, result.Color);
        Assert.NotEqual(request.RowVersion, result.RowVersion);
        
        // Assert - database state
        var animal = await dbContext.Animals.FirstOrDefaultAsync(a => a.Id == request.Id);
        
        Assert.NotNull(animal);
        Assert.Equal(request.Id, animal.Id);
        Assert.Equal(request.Name, animal.Name);
        Assert.Equal(request.Age, animal.Age);
        Assert.Equal(request.Weight, animal.Weight);
        Assert.Equal((Gender)request.Gender, animal.Gender);
        Assert.Equal((Color)request.Color, animal.Color);
        Assert.NotEqual(request.RowVersion, animal.RowVersion);
    }

    [Fact]
    public async Task Delete_ValidRequest_ShouldDeleteAnimal()
    {
        // Arrange
        using var scope = factory.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        var service = scope.ServiceProvider.GetRequiredService<IAnimalService>();
        var resultDto = await service.Create(new CreateAnimalRequest(Name: "Capibara", Age: 3, Weight: 5, Gender: 1, Color: 3));
        
        // Act
        await service.SoftDelete(resultDto.Id);
        
        // Assert
        var resultOfNormalQuery = await dbContext.Animals.FirstOrDefaultAsync(a => a.Id == resultDto.Id);
        var resultOfQueryIgnoringFilters = await dbContext.Animals
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(a => a.Id == resultDto.Id);
        
        Assert.Null(resultOfNormalQuery);
        Assert.NotNull(resultOfQueryIgnoringFilters);
    }
}