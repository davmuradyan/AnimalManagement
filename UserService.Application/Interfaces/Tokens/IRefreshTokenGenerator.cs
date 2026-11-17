namespace UserService.Application.Interfaces.Tokens;

public interface IRefreshTokenGenerator
{
    string Generate();
    string Hash(string refreshToken);
}