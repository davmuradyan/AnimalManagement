using AnimalManagement.Shared.Exceptions.Domain;
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
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Invalid user name.");
        
        if (string.IsNullOrWhiteSpace(surname))
            throw new DomainValidationException("Invalid surname.");
        
        if (string.IsNullOrWhiteSpace(username))
            throw new DomainValidationException("Invalid username.");
        
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainValidationException("Invalid password hash.");
        
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