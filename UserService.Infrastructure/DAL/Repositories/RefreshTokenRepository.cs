using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Infrastructure.DAL.Database;

namespace UserService.Infrastructure.DAL.Repositories;

public class RefreshTokenRepository(MainDbContext context) : IRefreshTokenRepository
{
    public async Task Create(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> Get(int id)
    {
        var refreshToken = await context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(t => t.Id == id);
        return refreshToken;
    }

    public async Task<RefreshToken?> Get(string refreshTokenHash)
    {
        var refreshToken = await context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(t => t.TokenHash == refreshTokenHash);
        return refreshToken;
    }

    public async Task<bool> TryRotate(RefreshToken oldToken, RefreshToken newToken)
    {
        try
        {
            RefreshToken.Revoke(oldToken);
            await context.RefreshTokens.AddAsync(newToken);
            context.RefreshTokens.Update(oldToken);
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException e)
        {
            return false;
        }
    }
}