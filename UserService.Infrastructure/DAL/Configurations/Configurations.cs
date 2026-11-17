using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.DAL.Configurations;

public static class Configurations
{
    public static ModelBuilder AddConfigurations(this ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new RefreshTokenConfiguration().Configure(modelBuilder.Entity<RefreshToken>());
        return modelBuilder;
    }
}