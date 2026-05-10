using cwiczenia5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cwiczenia5.Configurations;

public class ComponentTypeConfiguration : IEntityTypeConfiguration<ComponentType>
{
    public void Configure(EntityTypeBuilder<ComponentType> builder)
    {
        builder.HasKey(ct => ct.Id);
        builder.Property(c => c.Abbreviation).IsRequired().HasMaxLength(30);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);

        builder.ToTable("ComponentType");

        builder.HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
        );
    }
}