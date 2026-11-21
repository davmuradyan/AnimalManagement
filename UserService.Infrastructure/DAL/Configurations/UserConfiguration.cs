using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Username)
            .IsRequired();
        
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Surname).IsRequired();
        
        builder.Property(u => u.RowVersion)
            .IsRowVersion();

        builder.HasData(GetSeedData());
    }

    private User GetSeedData()
    {
        var result = new User
        {
            Id = 1,
            Name = "Davit",
            Surname = "Muradyan",
            Username = "testuser",
            PasswordHash = "$2y$12$K9Fx74bcVht7wJa7j2ka/ubruvsKxTCzu1MpAkbpIMV4LjouNwjLK",
            CreatedAt = new DateTime(2026, 11, 16, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2026, 11, 16, 0, 0, 0, DateTimeKind.Utc)
        };

    return result;
    }
}