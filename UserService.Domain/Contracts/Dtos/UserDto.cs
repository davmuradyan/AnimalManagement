namespace UserService.Domain.Contracts.Dtos;

public record UserDto(int Id, string Name, string Surname, string Username, byte[] rowVersion);