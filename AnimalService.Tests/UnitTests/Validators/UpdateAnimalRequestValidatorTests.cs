using AnimalService.Application.Services.AnimalServices.Validators;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;

namespace AnimalService.Tests.UnitTests.Validators;

public class UpdateAnimalRequestValidatorTests
{
    private readonly UpdateAnimalRequestValidator _validator = new();
    
    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new UpdateAnimalRequest(1, "Dog", 5, 20.3, 1, 2, [1,1,1,1,1,1,1,1]);

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_RequestWithInvalidId_ShouldBeInvalid(int invalidId)
    {
        // Arrange
        string validName = "Dog";
        int validAge = 1;
        double validWeight = 1.2;
        int validGender = 1;
        int validColor = 1;
        byte[] validRowVersion = new byte[8];
        var  request = new UpdateAnimalRequest(invalidId, validName, validAge, validWeight, validColor, validGender, validRowVersion);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void Validate_RequestWithInvalidName_ShouldBeInvalid(string invalidName)
    {
        // Arrange
        int vaildId = 1;
        int validAge = 1;
        double validWeight = 1.2;
        int validGender = 1;
        int validColor = 1;
        byte[] validRowVersion = new byte[8];
        var  request = new UpdateAnimalRequest(vaildId,invalidName, validAge, validWeight, validColor, validGender, validRowVersion);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void Validate_RequestWithInvalidAge_ShouldBeInvalid()
    {
        // Arrange
        int validId = 1;
        string validName = "Dog";
        int invalidAge = -1;
        double validWeight = 1.2;
        int validGender = 1;
        int validColor = 1;
        byte[] validRowVersion = new byte[8];
        var  request = new UpdateAnimalRequest(validId, validName, invalidAge, validWeight, validColor, validGender, validRowVersion);
        
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
        int validId = 1;
        string validName = "Dog";
        int validAge = 1;
        int validGender = 1;
        int validColor = 1;
        byte[] validRowVersion = new byte[8];
        var  request = new UpdateAnimalRequest(validId,validName, validAge, invalidWeight, validColor, validGender, validRowVersion);
        
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
        int validId = 1;
        string validName = "Dog";
        int validAge = 1;
        double validWeight = 1.2;
        int validColor = 1;
        byte[] validRowVersion = new byte[8];
        var  request = new UpdateAnimalRequest(validId,validName, validAge, validWeight, validColor,invalidGender, validRowVersion);
        
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
        int validId = 1;
        string validName = "Dog";
        int validAge = 1;
        double validWeight = 1.2;
        int validGender = 1;
        byte[] validRowVersion = new byte[8];
        var  request = new UpdateAnimalRequest(validId, validName, validAge, validWeight, invalidColor, validGender, validRowVersion);
        
        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 0, 0, 0 })]
    [InlineData(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
    public void Validate_InvalidRowVersion_ShouldBeInvalid(byte[] invalidRowVersion)
    {
        // Arrange
        var request = new UpdateAnimalRequest(
            1, "Dog", 1, 1.2, 1, 1, invalidRowVersion);

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(UpdateAnimalRequest.RowVersion));
    }
}