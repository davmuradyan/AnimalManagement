namespace AnimalService.Domain.Contracts.Requests.AnimalRequests;

public record CreateAnimalRequest(string Name, int Age, double Weight, string Gender, string Color, int Language);
