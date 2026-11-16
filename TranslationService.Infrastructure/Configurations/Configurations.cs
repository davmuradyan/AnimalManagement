using Microsoft.EntityFrameworkCore;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Configurations;

public static class Configurations
{
    public static ModelBuilder AddConfigurations(this ModelBuilder modelBuilder)
    {
        new EnumTranslationConfiguration().Configure(modelBuilder.Entity<EnumTranslation>());

        return modelBuilder;
    }
}