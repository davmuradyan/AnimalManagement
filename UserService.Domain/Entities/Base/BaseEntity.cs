namespace UserService.Domain.Entities.Base;

public class BaseEntity
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CloseDate { get; set; }
}