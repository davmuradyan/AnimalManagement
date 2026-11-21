namespace TranslationService.Domain.Entities.Base;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } =  DateTime.Now;
    public DateTime? ClosedDate { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    public void Close()
    {
        ClosedDate = DateTime.Now;
    }
}