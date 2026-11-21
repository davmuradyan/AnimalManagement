namespace AnimalService.Domain.Contracts.Dtos;

public record AnimalDto
{
    public int Id { get; init; }
    public string Name { get; init; } 
    public int Age { get; init; }
    public double Weight { get; init; }
    public string Gender { get; set; }
    public string Color { get; set; }
    public byte[] RowVersion { get; set; }
}