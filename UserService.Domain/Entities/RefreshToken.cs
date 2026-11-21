namespace UserService.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string TokenHash {  get; set; }
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);
    public DateTime? RevokedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatdAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User? User { get; set; }
}