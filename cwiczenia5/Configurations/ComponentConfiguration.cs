using cwiczenia5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cwiczenia5.Configurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.HasKey(c => c.Code);
            
        builder.Property(c => c.Code).IsRequired().HasColumnType("char").HasMaxLength(10);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Description).IsRequired();
            
        builder.HasOne(c => c.ComponentType)
            .WithMany(c => c.Components)
            .HasForeignKey(c => c.ComponentTypesId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(c => c.ComponentManufacturer)
            .WithMany(c => c.Components)
            .HasForeignKey(c => c.ComponentManufacturersId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Component");

        builder.HasData(
            new Component { Code = "CPU0000001", Name = "Ryzen 7 7700X", Description = "8-core desktop processor", ComponentTypesId = 1, ComponentManufacturersId = 1 },
            new Component { Code = "GPU0000001", Name = "GeForce RTX 4070", Description = "Mid-range gaming GPU", ComponentTypesId = 2, ComponentManufacturersId = 2 },
            new Component { Code = "RAM0000001", Name = "Vengeance 16GB DDR5", Description = "DDR5 5600 MHz memory module", ComponentTypesId = 3, ComponentManufacturersId = 3 }
        );
    }
}