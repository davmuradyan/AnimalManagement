namespace AnimalService.Domain.Contracts.Requests.AnimalRequests;

public record UpdateAnimalRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public double Weight { get; set; }
    public string Color { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string SelectedLanguage { get; set; } = null!;
}