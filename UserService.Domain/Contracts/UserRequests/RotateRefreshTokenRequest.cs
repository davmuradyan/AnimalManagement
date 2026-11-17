namespace UserService.Domain.Contracts.UserRequests;

public record RotateRefreshTokenRequest(string OldRefreshToken);