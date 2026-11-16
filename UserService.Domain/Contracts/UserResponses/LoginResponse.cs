namespace UserService.Domain.Contracts.UserResponses;

public record LoginResponse(string Token, DateTime ExpiresAt);
