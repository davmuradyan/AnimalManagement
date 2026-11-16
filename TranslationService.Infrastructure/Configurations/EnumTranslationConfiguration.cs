using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Configurations;

public class EnumTranslationConfiguration
    : IEntityTypeConfiguration<EnumTranslation>
{
    public void Configure(EntityTypeBuilder<EnumTranslation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.EnumType,
            x.EnumCode,
            x.Language
        }).IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EnumType)
            .IsRequired();

        builder.Property(x => x.Language)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Translation)
            .IsRequired();
        
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
        
        builder.HasData(GetSeedData());
    }
    
    private static IEnumerable<EnumTranslation> GetSeedData()
    {
        var result = new List<EnumTranslation>();
        var id = 1;

        void Add(string type, int code, string eng, string hy)
        {
            result.Add(new EnumTranslation
            {
                Id = id++,
                EnumType = type,
                EnumCode = code,
                Language = Language.Eng,
                Translation = eng
            });

            result.Add(new EnumTranslation
            {
                Id = id++,
                EnumType = type,
                EnumCode = code,
                Language = Language.Hy,
                Translation = hy
            });
        }

        Add(nameof(Color), (int)Color.Red, "Red", "Կարմիր");
        Add(nameof(Color), (int)Color.Green, "Green", "Կանաչ");
        Add(nameof(Color), (int)Color.Blue, "Blue", "Կապույտ");
        Add(nameof(Color), (int)Color.Yellow, "Yellow", "Դեղին");
        Add(nameof(Color), (int)Color.Orange, "Orange", "Նարնջագույն");
        Add(nameof(Color), (int)Color.Purple, "Purple", "Մանուշակագույն");
        Add(nameof(Color), (int)Color.Pink, "Pink", "Վարդագույն");
        Add(nameof(Color), (int)Color.Black, "Black", "Սև");
        Add(nameof(Color), (int)Color.White, "White", "Սպիտակ");
        Add(nameof(Color), (int)Color.Brown, "Brown", "Շագանակագույն");

        Add(nameof(Gender), (int)Gender.Male, "Male", "Արու");
        Add(nameof(Gender), (int)Gender.Female, "Female", "Էգ");

        return result;
    }
}