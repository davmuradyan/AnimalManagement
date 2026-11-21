using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Configurations;

public class TranslationGroupConfiguration : IEntityTypeConfiguration<TranslationGroup>
{
    public void Configure(EntityTypeBuilder<TranslationGroup> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.HasData(InitialData());
    }

    private List<TranslationGroup> InitialData() =>
    [
        new TranslationGroup(nameof(TranslationGroups.Color), false)
        {
            Id = 1,
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue
        },
        new TranslationGroup(nameof(TranslationGroups.Gender), false)
        {
            Id = 2,
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue
        },
        new TranslationGroup(nameof(TranslationGroups.Language), false)
        {
            Id = 3,
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue
        }
    ];
}