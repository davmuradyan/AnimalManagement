namespace UserService.Domain.Contracts.UserResponses;

public record LoginResponse(string AccessToken, string RefreshToken);
