using cwiczenia5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cwiczenia5.Configurations;

public class ComponentManufacturerConfiguration : IEntityTypeConfiguration<ComponentManufacturer>
{
    public void Configure(EntityTypeBuilder<ComponentManufacturer> builder)
    {
        builder.HasKey(cm => cm.Id);
            
        builder.Property(cm => cm.Abbreviation).IsRequired().HasMaxLength(30);
        builder.Property(cm => cm.FullName).IsRequired().HasMaxLength(300);
        builder.Property(cm => cm.FoundationDate).IsRequired().HasColumnType("date");

        builder.ToTable("ComponentManufacturer");

        builder.HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices, Inc.", FoundationDate = new DateTime(1969, 5, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NVIDIA", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
            new ComponentManufacturer { Id = 3, Abbreviation = "CORSAIR", FullName = "Corsair Gaming, Inc.", FoundationDate = new DateTime(1994, 1, 1) }
        );
    }
}