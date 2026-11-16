namespace UserService.Domain.Entities.Base;

public class BaseEntity
{
    public int Id { get; init; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? CloseDate { get; set; }
}