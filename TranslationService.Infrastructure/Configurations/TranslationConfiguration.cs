using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Configurations;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LanguageTag)
            .IsRequired();
        
        builder.Property(x => x.TranslatedText)
            .IsRequired();

        builder.HasOne<GroupItem>(x => x.GroupItemNavigation)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.GroupItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x => x.LanguageTag)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
        
        builder.HasData(InitializeData());
    }

    private List<Translation> InitializeData() =>
    [
        // Colors
        new Translation(1, Language.En, "Red")      { Id = 1, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(1, Language.Hy, "Կարմիր")   { Id = 2, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        new Translation(2, Language.En, "Green")    { Id = 3, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(2, Language.Hy, "Կանաչ")    { Id = 4, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        new Translation(3, Language.En, "Blue")     { Id = 5, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(3, Language.Hy, "Կապույտ")  { Id = 6, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        // Genders
        new Translation(4, Language.En, "Male")     { Id = 7, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(4, Language.Hy, "Արու")   { Id = 8, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        new Translation(5, Language.En, "Female")   { Id = 9, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(5, Language.Hy, "Էգ")   { Id = 10, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        // Languages
        new Translation(6, Language.En, "English")  { Id = 11, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(6, Language.Hy, "Անգլերեն") { Id = 12, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        new Translation(7, Language.En, "Armenian") { Id = 13, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new Translation(7, Language.Hy, "Հայերեն")  { Id = 14, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue }
    ];
}