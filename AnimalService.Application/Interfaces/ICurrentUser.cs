namespace AnimalService.Application.Interfaces;

public interface ICurrentUser
{
    string? UserId { get; }
    string? Username { get; }
}