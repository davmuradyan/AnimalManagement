using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using AnimalService.Domain.Entities.Base;

namespace AnimalService.Domain.Entities;

public class Animal : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public double Weight { get; set; }
    public Gender Gender { get; set; }
    public Color Color { get; set; }
    public string CreatedByUsername { get; set; } = string.Empty;
    public string UpdatedByUsername { get; set; } = string.Empty;

    public void Close(string username)
    {
        ClosedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        UpdatedByUsername = username;
    }
}