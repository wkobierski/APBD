using cwiczenia5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cwiczenia5.Configurations;

public class PcComponentConfiguration : IEntityTypeConfiguration<PcComponent>
{
    public void Configure(EntityTypeBuilder<PcComponent> builder)
    {
        builder.HasKey(pcc => new { pcc.PcId, pcc.ComponentCode });
            
        builder.HasOne(pcc => pcc.Pc)
            .WithMany(pcc => pcc.PcComponents)
            .HasForeignKey(pcc => pcc.PcId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(pcc => pcc.Component)
            .WithMany(cc => cc.PcComponents)
            .HasForeignKey(pcc => pcc.ComponentCode)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.Property(pcc => pcc.Amount).IsRequired();

        builder.ToTable("PCComponent");

        builder.HasData(
            new PcComponent { PcId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PcComponent { PcId = 2, ComponentCode = "GPU0000001", Amount = 1 },
            new PcComponent { PcId = 3, ComponentCode = "RAM0000001", Amount = 4 }
        );
    }
}