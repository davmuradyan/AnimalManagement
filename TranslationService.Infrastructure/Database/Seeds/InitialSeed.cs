using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Database.Seeds;

public static class InitialSeed
{
    public static async Task SeedEnumTranslations(
        this MainDbContext context)
    {
        if (await context.EnumTranslations.AnyAsync())
            return;

        var translations = new List<EnumTranslation>();

        void Add(string type, int code, string eng, string hy)
        {
            translations.Add(new EnumTranslation
            {
                EnumType = type,
                EnumCode = code,
                Language = Language.Eng,
                Translation = eng
            });

            translations.Add(new EnumTranslation
            {
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

        await context.EnumTranslations.AddRangeAsync(translations);
        await context.SaveChangesAsync();
    }
}