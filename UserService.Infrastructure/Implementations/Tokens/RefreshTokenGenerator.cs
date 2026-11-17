using System.Security.Cryptography;
using UserService.Application.Interfaces.Tokens;

namespace UserService.Infrastructure.Implementations.Tokens;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(1024);
        return Convert.ToBase64String(bytes);
    }

    public string Hash(string token)
    {
        var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(token));

        return Convert.ToHexString(bytes);
    }
}