using AnimalService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Infrastructure.DAL.Configurations;

public static class Configurations
{
    public static ModelBuilder AddConfigurations(this ModelBuilder modelBuilder)
    {
        new AnimalConfiguration().Configure(modelBuilder.Entity<Animal>());

        return modelBuilder;
    }
}