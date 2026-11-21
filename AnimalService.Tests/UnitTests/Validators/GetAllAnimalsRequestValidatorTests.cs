using AnimalService.Application.Services.AnimalServices.Validators;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;

namespace AnimalService.Tests.UnitTests.Validators;

public class GetAllAnimalsRequestValidatorTests
{
    private readonly GetAllAnimalsRequestValidator _validator = new();

    [Theory]
    [InlineData(1, 10, 10, false, new[] { "Blue" }, 1, 10, 1.5, 20.5, "Male")]
    [InlineData(2, 20, 20, true, new[] { "Red", "Green" }, null, null, null, null, null)]
    public void Validate_ValidRequest_ShouldBeValid(
        int page,
        int pageSize,
        int count,
        bool includeClosed,
        string[] colors,
        int? minAge,
        int? maxAge,
        double? minWeight,
        double? maxWeight,
        string? gender)
    {
        // Arrange
        var request = new GetAllAnimalsRequest
        {
            Page = page,
            PageSize = pageSize,
            Count = count,
            IncludeClosed = includeClosed,
            Colors = colors,
            MinAge = minAge,
            MaxAge = maxAge,
            MinWeight = minWeight,
            MaxWeight = maxWeight,
            Gender = gender
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_RequestWithInvalidPage_ShouldBeValid(int invalidPage)
    {
        // Arrange
        var request = new GetAllAnimalsRequest
        {
            Page = invalidPage,
            PageSize = 10,
            Count = 10,
            IncludeClosed = false,
            Colors = [],
            MinAge = null,
            MaxAge = null,
            MinWeight = null,
            MaxWeight = null,
            Gender = null
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_RequestWithInvalidPageSize_ShouldBeValid(int invalidPageSize)
    {
        // Arrange
        var request = new GetAllAnimalsRequest
        {
            Page = 1,
            PageSize = invalidPageSize,
            Count = 10,
            IncludeClosed = false,
            Colors = [],
            MinAge = null,
            MaxAge = null,
            MinWeight = null,
            MaxWeight = null,
            Gender = null
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_RequestWithInvalidCount_ShouldBeValid(int invalidCount)
    {
        // Arrange
        var request = new GetAllAnimalsRequest
        {
            Page = 1,
            PageSize = 10,
            Count = invalidCount,
            IncludeClosed = false,
            Colors = [],
            MinAge = null,
            MaxAge = null,
            MinWeight = null,
            MaxWeight = null,
            Gender = null
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 1)]
    [InlineData(2, 1)]
    public void Validate_RequestWithInvalidMinAgeMaxAge_ShouldBeValid(int invalidMinAge, int invalidMaxAge)
    {
        // Arrange
        var request = new GetAllAnimalsRequest
        {
            Page = 1,
            PageSize = 10,
            Count = 10,
            IncludeClosed = false,
            Colors = [],
            MinAge = invalidMinAge,
            MaxAge = invalidMaxAge,
            MinWeight = null,
            MaxWeight = null,
            Gender = null
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 1)]
    [InlineData(0, 0)]
    [InlineData(2, 1)]
    public void Validate_RequestWithInvalidMinAgeMaxWeight_ShouldBeValid(int invalidMinWeight, int invalidMaxWeight)
    {
        // Arrange
        var request = new GetAllAnimalsRequest
        {
            Page = 1,
            PageSize = 10,
            Count = 10,
            IncludeClosed = false,
            Colors = [],
            MinAge = null,
            MaxAge = null,
            MinWeight = invalidMinWeight,
            MaxWeight = invalidMaxWeight,
            Gender = null
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
}