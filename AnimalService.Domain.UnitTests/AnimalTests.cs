using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Entities;
using AnimalService.Domain.Exceptions;

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
        
        // Act
        var act = () => Animal.Create(invalidAnimalName, validAge, validWeiht, validGender, validColor);
        
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
        
        // Act
        var act = () => Animal.Create(validName, invalidAge, validWeiht, validGender, validColor);
        
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
        
        // Act
        var act = () => Animal.Create(validName, validAge, invalidWeight, validGender, validColor);
        
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
        
        // Act
        var act = () => Animal.Create(validName, validAge, validWeiht, validGender, invalidColor);
        
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
        
        // Act
        var act = () => Animal.Create(validName, validAge, validWeiht, invalidGender, validColor);
        
        // Asert
        Assert.Throws<DomainValidationException>(act);
    }

    [Theory]
    [InlineData("Aba ", 10, 3.4, Gender.Male, Color.Red)]
    [InlineData("K", 10, 3.4, Gender.Male, Color.Brown)]
    [InlineData("aA5 3!-*/+!@#$%^&*()_", 0, 100000000, Gender.Female, Color.Black)]
    public void Create_WithValidParameters_ShouldCreate(string validName, int validAge, double validWeight, Gender validGender, Color validColor)
    {
        // Act
        Animal animal = Animal.Create(validName, validAge, validWeight, validGender, validColor);

        // Assert
        Assert.Equal(validName, animal.Name);
        Assert.Equal(validAge, animal.Age);
        Assert.Equal(validWeight, animal.Weight);
        Assert.Equal(validGender, animal.Gender);
        Assert.Equal(validColor, animal.Color);

        Assert.IsType<Animal>(animal);
    }
}