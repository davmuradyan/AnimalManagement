using AnimalService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalService.Infrastructure.DAL.Configurations;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired();
        
        builder.Property(a => a.CreatedByUsername)
            .IsRequired();
        
        builder.Property(a => a.UpdatedByUsername)
            .IsRequired();

        builder.Property(a => a.Color)
            .HasConversion<string>();

        builder.Property(a => a.Gender)
            .HasConversion<string>();

        builder.Property(a => a.ClosedDate);

        builder.ToTable(t => t.HasCheckConstraint("CK_Animal_Weight", "[Weight] > 0"));

        builder.ToTable(t => t.HasCheckConstraint("CK_Animal_Age", "[Age] > 0"));

        builder.Property(a => a.RowVersion)
            .IsRowVersion();
        
        builder.HasQueryFilter(a => a.ClosedDate == null);
    }
}