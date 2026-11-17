using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task Create(RefreshToken refreshToken);
    Task<RefreshToken?> Get(int id);
    Task<RefreshToken?> Get(string refreshTokenHash);
    Task<bool> TryRotate(RefreshToken oldToken, RefreshToken newToken);
}