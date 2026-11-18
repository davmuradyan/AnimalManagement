using AnimalManagement.Shared.Enums;
using AnimalManagement.Shared.Exceptions.Domain;
using AnimalService.Domain.Entities;

namespace AnimalService.Domain.UnitTests;

public class AnimalTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Create_WithInvalidName_ShouldThrow(string invalidAnimalName)
    {
        // Arrange
        int validAge = 10;
        double validWeiht = 1.1;
        Gender validGender = Gender.Male;
        Color validColor = Color.Red;
        string validUsername = "test-user";
        
        // Act
        var act = () => Animal.Create(invalidAnimalName, validAge, validWeiht, validGender, validColor, validUsername);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Create_WithInvalidCreatedByUserName_ShouldThrow(string invalidCreatedByUserName)
    {
        // Arrange
        string validName = "Kitty";
        int validAge = 10;
        double validWeiht = 1.1;
        Gender validGender = Gender.Male;
        Color validColor = Color.Red;
        
        // Act
        var act = () => Animal.Create(validName, validAge, validWeiht, validGender, validColor, invalidCreatedByUserName);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public void Create_WithInvalidAge_ShouldThrow(int invalidAge)
    {
        // Arrange
        string validName = "Kitty";
        double validWeiht = 1.1;
        Gender validGender = Gender.Male;
        Color validColor = Color.Red;
        string validUsername = "test-user";
        
        // Act
        var act = () => Animal.Create(validName, invalidAge, validWeiht, validGender, validColor, validUsername);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MaxValue)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Create_WithInvalidWeight_ShouldThrow(double invalidWeight)
    {
        // Arrange
        string validName = "Kitty";
        int validAge = 1;
        Gender validGender = Gender.Male;
        Color validColor = Color.Red;
        string validUsername = "test-user";
        
        // Act
        var act = () => Animal.Create(validName, validAge, invalidWeight, validGender, validColor, validUsername);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }

    [Fact]
    public void Create_WithInvalidColor_ShouldThrow()
    {
        // Arrange
        string validName = "Kitty";
        int validAge = 1;
        double validWeiht = 1.1;
        Gender validGender = Gender.Male;
        Color invalidColor = (Color)100000;
        string validUsername = "test-user";
        
        // Act
        var act = () => Animal.Create(validName, validAge, validWeiht, validGender, invalidColor, validUsername);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }

    [Fact]
    public void Create_WithInvalidGender_ShouldThrow()
    {
        // Arrange
        string validName = "Kitty";
        int validAge = 1;
        double validWeiht = 1.1;
        Gender invalidGender = (Gender)100000;
        Color validColor = Color.Black;
        string validUsername = "test-user";
        
        // Act
        var act = () => Animal.Create(validName, validAge, validWeiht, invalidGender, validColor, validUsername);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }

    [Theory]
    [InlineData("Aba ", 10, 3.4, Gender.Male, Color.Red, "test-user")]
    [InlineData("K", 10, 3.4, Gender.Male, Color.Brown, "test")]
    [InlineData("aA5 3!-*/+!@#$%^&*()_", 0, 100000000, Gender.Female, Color.Black, "1test1")]
    public void Create_WithValidParameters_ShouldCreate(string validName, int validAge, double validWeight, Gender validGender, Color validColor, string validUsername)
    {
        // Act
        Animal animal = Animal.Create(validName, validAge, validWeight, validGender, validColor, validUsername);

        // Assert
        Assert.Equal(validName, animal.Name);
        Assert.Equal(validAge, animal.Age);
        Assert.Equal(validWeight, animal.Weight);
        Assert.Equal(validGender, animal.Gender);
        Assert.Equal(validColor, animal.Color);
        Assert.Equal(validUsername, animal.CreatedByUsername);
        Assert.Equal(validUsername, animal.UpdatedByUsername);

        Assert.IsType<Animal>(animal);
    }
}