namespace AnimalService.Domain.Contracts.Requests.AnimalRequests;

public record UpdateAnimalRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public double Weight { get; set; }
    public int Color { get; set; }
    public int Gender { get; set; }
    public byte[] RowVersion { get; set; }
    public int SelectedLanguage { get; set; }
}