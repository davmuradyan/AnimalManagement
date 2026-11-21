using UserService.Domain.Entities.Base;

namespace UserService.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}