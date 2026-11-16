namespace TranslationService.Domain.Entities.Base;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; protected init; }
    public DateTime UpdatedAt { get; protected set; }
    public DateTime? ClosedDate { get; protected set; }
    public byte[] RowVersion { get; set; } = null!;
}