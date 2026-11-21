namespace AnimalService.Domain.Contracts.Requests.AnimalRequests;

public record UpdateAnimalRequest(int Id, string Name, int Age, double Weight, int Color, int Gender, byte[] RowVersion);