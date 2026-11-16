using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Entities;

namespace AnimalService.Application;

public static class Helpers
{
    internal static AnimalDto MapToAnimalDto(this Animal animal, string gender, string color)
    {
        return new AnimalDto(
            animal.Id,
            animal.Name,
            animal.Age,
            animal.Weight,
            gender,
            color);
    }
}