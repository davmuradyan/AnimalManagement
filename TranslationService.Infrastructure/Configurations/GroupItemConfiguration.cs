using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranslationService.Domain.Entities;

namespace TranslationService.Infrastructure.Configurations;

public class GroupItemConfiguration : IEntityTypeConfiguration<GroupItem>
{
    public void Configure(EntityTypeBuilder<GroupItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne<TranslationGroup>(x => x.GroupNavigation)
            .WithMany(g => g.GroupItems)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.HasData(InitializeData());
    }

    private List<GroupItem> InitializeData() =>
    [
        // Colors (GroupId = 1)
        new GroupItem(1, (int)Color.Red) { Id = 1, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new GroupItem(1, (int)Color.Green) { Id = 2, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new GroupItem(1, (int)Color.Blue) { Id = 3, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        // Genders (GroupId = 2)
        new GroupItem(2, (int)Gender.Male) { Id = 4, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new GroupItem(2, (int)Gender.Female) { Id = 5, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },

        // Languages (GroupId = 3)
        new GroupItem(3, (int)Language.En) { Id = 6, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue },
        new GroupItem(3, (int)Language.Hy) { Id = 7, CreatedAt = DateTime.MinValue, UpdatedAt = DateTime.MinValue }
    ];
}