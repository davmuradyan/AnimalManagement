using UserService.Application.Interfaces.HashFunctions;

namespace UserService.Infrastructure.Implementations.HashFunctions;

public class BCryptHashService : IPasswordHashService
{
    public string Hash(string value) =>
        BCrypt.Net.BCrypt.HashPassword(value, workFactor: 12);

    public bool Verify(string value, string hash) =>
        BCrypt.Net.BCrypt.Verify(value, hash);
}