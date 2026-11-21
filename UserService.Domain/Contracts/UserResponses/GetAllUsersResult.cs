using UserService.Domain.Contracts.Dtos;

namespace UserService.Domain.Contracts.UserResponses;

public record GetAllUsersResult(List<UserDto> Users);