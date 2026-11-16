using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Tokens;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}