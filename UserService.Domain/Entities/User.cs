using UserService.Domain.Entities.Base;

namespace UserService.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public User() {}

    public static User Create(string name, string surname, string username, string passwordHash)
    {
        return new()
        {
            Name = name,
            Surname = surname,
            Username = username,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}