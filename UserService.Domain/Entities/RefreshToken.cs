using UserService.Domain.Entities.Base;
using UserService.Domain.Exceptions;

namespace UserService.Domain.Entities;

public class RefreshToken
{
    public int Id { get; init; }
    public string TokenHash {  get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public byte[] RowVersion { get; set; } = null!;
    public DateTime CreatdAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; private set; }
    public User? User { get; set; }
    
    private RefreshToken() {}

    public static RefreshToken Create(string tokenHash, int userId)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new DomainValidationException("The provided Token Hash was empty.");

        return new()
        {
            TokenHash = tokenHash,
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
    }

    public static void Revoke(RefreshToken token)
    {
        if (token == null)
            throw new DomainValidationException("The provided Refresh Token was null.");
        
        token.RevokedAt = DateTime.UtcNow;
    }
}