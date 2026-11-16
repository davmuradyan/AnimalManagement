using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Entities.Base;
using AnimalService.Domain.Exceptions;

namespace AnimalService.Domain.Entities;

public class Animal : BaseEntity
{
    public string Name { get; private set; } = null!;
    public int Age { get; private set; }
    public double Weight { get; private set; }
    public Gender Gender { get; private set; }
    public Color Color { get; private set; }
    private Animal() {}
    public static Animal Create(
        string name,
        int age,
        double weight,
        Gender gender,
        Color color)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Animal name cannot be empty.");

        if (!Enum.IsDefined(gender))
            throw new DomainValidationException("Invalid animal gender.");

        if (!Enum.IsDefined(color))
            throw new DomainValidationException("Invalid animal color.");

        if (age < 0)
            throw new DomainValidationException($"Animal age cannot be negative. Input number was {age}.");

        if (weight <= 0 || double.IsNaN(weight) || double.IsInfinity(weight))
            throw new DomainValidationException($"Weight of animal must be positive double number. Input number was {weight}.");

        return new()
        {
            Name = name,
            Age = age,
            Weight = weight,
            Gender = gender,
            Color = color,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, int age, double weight, Gender gender, Color color)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Animal name cannot be empty.");

        if (!Enum.IsDefined(gender))
            throw new DomainValidationException("Invalid animal gender.");

        if (!Enum.IsDefined(color))
            throw new DomainValidationException("Invalid animal color.");

        if (age < 0)
            throw new DomainValidationException($"Animal age cannot be negative. Input number was {age}.");

        if (weight <= 0 || double.IsNaN(weight) || double.IsInfinity(weight))
            throw new DomainValidationException($"Weight of animal must be positive double number. Input number was {weight}.");
        
        Name = name;
        Age = age;
        Weight = weight;
        Gender = gender;
        Color = color;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Close()
    {
        if (ClosedDate.HasValue)
            throw new DomainValidationException("Animal is already closed.");

        var now = DateTime.UtcNow;

        UpdatedAt = now;
        ClosedDate = now;
    }
}