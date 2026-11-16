using AnimalManagement.Shared.Enums;

namespace AnimalService.Domain.HelperClasses;

public class FilterArgs
{
    public int? Count { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public double? MinWeight { get; set; }
    public double? MaxWeight { get; set; }
    public Color[] Colors { get; set; } = [];
    public Gender? Gender { get; set; }
    public bool IncludeClosed { get; set; } = false;
}