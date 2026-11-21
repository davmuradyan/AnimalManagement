using Microsoft.EntityFrameworkCore;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Configurations;

public static class Configurations
{
    public static ModelBuilder AddConfigurations(this ModelBuilder modelBuilder)
    {
        new TranslationGroupConfiguration().Configure(modelBuilder.Entity<TranslationGroup>());
        new GroupItemConfiguration().Configure(modelBuilder.Entity<GroupItem>());
        new TranslationConfiguration().Configure(modelBuilder.Entity<Translation>());
        return modelBuilder;
    }
}