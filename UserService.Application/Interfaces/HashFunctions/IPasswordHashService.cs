namespace UserService.Application.Interfaces.HashFunctions;

public interface IPasswordHashService
{
    string Hash(string value);
    bool Verify(string value, string hash);
}