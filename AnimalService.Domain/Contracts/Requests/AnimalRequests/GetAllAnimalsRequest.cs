namespace AnimalService.Domain.Contracts.Requests.AnimalRequests;

public record GetAllAnimalsRequest
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Count { get; init; }
    public bool IncludeClosed { get; init; }
    public string[] Colors { get; init; } = [];
    public int? MinAge { get; init; }
    public int? MaxAge { get; init; }
    public double? MinWeight { get; init; }
    public double? MaxWeight { get; init; }
    public string? Gender { get; init; }
}