using Microsoft.EntityFrameworkCore;
using UserService.Domain;
using UserService.Domain.Entities;
using UserService.Infrastructure.DAL.Configurations;

namespace UserService.Infrastructure.Configurations;

public static class Configurations
{
    public static ModelBuilder AddConfigurations(this ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());

        return modelBuilder;
    }
}