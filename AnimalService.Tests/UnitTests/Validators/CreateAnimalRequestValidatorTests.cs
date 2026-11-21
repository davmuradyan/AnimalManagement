using AnimalService.Application.Services.AnimalServices.Validators;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;

namespace AnimalService.Tests.UnitTests.Validators;

public class CreateAnimalRequestValidatorTests()
{
    private readonly CreateAnimalRequestValidator _validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new CreateAnimalRequest("Rex", 5, 20.3, 1, 3);

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }
    
    //public record CreateAnimalRequest(string Name, int Age, double Weight, int Gender, int Color);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void Validate_RequestWithInvalidName_ShouldBeInvalid(string invalidName)
    {
        // Arrange
        int validAge = 1;
        double validWeight = 1.2;
        int validGender = 1;
        int validColor = 1;
        var  request = new CreateAnimalRequest(invalidName, validAge, validWeight, validGender, validColor);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void Validate_RequestWithInvalidAge_ShouldBeInvalid()
    {
        // Arrange
        string validName = "Dog";
        int invalidAge = -1;
        double validWeight = 1.2;
        int validGender = 1;
        int validColor = 1;
        var  request = new CreateAnimalRequest(validName, invalidAge, validWeight, validGender, validColor);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NaN)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_RequestWithInvalidWeight_ShouldBeInvalid(double invalidWeight)
    {
        // Arrange
        string validName = "Dog";
        int validAge = 1;
        int validGender = 1;
        int validColor = 1;
        var  request = new CreateAnimalRequest(validName, validAge, invalidWeight, validGender, validColor);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public void Validate_RequestWithInvalidGender_ShouldBeInvalid(int invalidGender)
    {
        // Arrange
        string validName = "Dog";
        int validAge = 1;
        double validWeight = 1.2;
        int validColor = 1;
        var  request = new CreateAnimalRequest(validName, validAge, validWeight, invalidGender, validColor);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(4)]
    public void Validate_RequestWithInvalidColor_ShouldBeInvalid(int invalidColor)
    {
        // Arrange
        string validName = "Dog";
        int validAge = 1;
        double validWeight = 1.2;
        int validGender = 1;
        var  request = new CreateAnimalRequest(validName, validAge, validWeight, validGender, invalidColor);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
}