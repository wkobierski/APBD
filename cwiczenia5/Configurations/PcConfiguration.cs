using cwiczenia5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cwiczenia5.Configurations;

public class PcConfiguration : IEntityTypeConfiguration<Pc>
{
    public void Configure(EntityTypeBuilder<Pc> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Name).IsRequired().HasMaxLength(50);
        builder.Property(pc => pc.Weight).IsRequired().HasColumnType("float").HasPrecision(5);
        builder.Property(pc => pc.Warranty).IsRequired();
        builder.Property(pc => pc.CreatedAt).IsRequired();
        builder.Property(pc => pc.Stock).IsRequired();

        builder.ToTable("PC");

        builder.HasData(
            new Pc { Id = 1, Name = "Gaming Rig X1", Weight = 12.5f, Warranty = 24, CreatedAt = new DateTime(2025, 1, 15), Stock = 10 },
            new Pc { Id = 2, Name = "Office Pro M2", Weight = 8.0f, Warranty = 12, CreatedAt = new DateTime(2025, 3, 20), Stock = 25 },
            new Pc { Id = 3, Name = "Workstation Z3", Weight = 15.2f, Warranty = 36, CreatedAt = new DateTime(2025, 5, 5), Stock = 5 }
        );
    }
}